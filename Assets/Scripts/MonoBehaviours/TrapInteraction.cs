using UnityEngine;
using System.Collections;

public class TrapInteraction : MonoBehaviour 
{
    public Trap trap;
    private Animator anim;
    private Animation currentAnim;
    string animationState = "AnimationState";
    private Vector3 pos;

    void Start()
    {
        currentAnim = GetComponent<Animation>();
        anim = GetComponent<Animator>();
        anim.SetFloat("animspeed", 0f);
        pos = gameObject.transform.position;
        pos.z = -100;
        gameObject.transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        pos = gameObject.transform.position;
        pos.z = 0;
        gameObject.transform.position = pos;


        StartCoroutine(MyCoroutine(pos));
        anim.SetFloat("animspeed", 1f);
    }

    IEnumerator MyCoroutine(Vector3 pos)
    {
        yield return new WaitForSeconds(2f);

        if(trap.trapName == "ZeusMainTrap")
        {
            gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
            Debug.Log(pos);

            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            Vector3 northEast = new Vector3(x + 1, y + 1, z);
            Vector3 northWest = new Vector3(x - 1, y + 1, z);
            Vector3 southEast = new Vector3(x + 1, y - 1, z);
            Vector3 southWest = new Vector3(x - 1, y - 1, z);
            /*createDiagonal(northEast);
            createDiagonal(northWest);
            createDiagonal(southEast);
            createDiagonal(southWest);*/
            Debug.Log(northEast);
        }

        anim.SetInteger(animationState, 1);
        Debug.Log(trap.trapName);

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
       
        yield return 0;
    }

}
