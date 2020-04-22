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
            Vector3 ninety = new Vector3(90,0,0);
            Vector3 northEast = new Vector3(x + 1, y + 1, z);
            Vector3 northWest = new Vector3(x - 1, y + 1, z);
            Vector3 southEast = new Vector3(x + 1, y - 1, z);
            Vector3 southWest = new Vector3(x - 1, y - 1, z);
            GameObject zeusSouthEast = (GameObject)Resources.Load("Zeus/ZeusSouthEast", typeof(GameObject));
            GameObject actualSouthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y - 1.5f, z), Quaternion.identity);
            GameObject actualSouthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y - 1.5f, z), Quaternion.Euler(0,0,0));
            GameObject actualNorthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y + 0.5f, z), Quaternion.identity);
            GameObject actualNorthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y + 0.5f, z), Quaternion.identity);
        }

        anim.SetInteger(animationState, 1);
        Debug.Log(trap.trapName);

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
       
        yield return 0;
    }

}
