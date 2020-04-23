using UnityEngine;
using System.Collections;

public class TrapInteraction : MonoBehaviour 
{
    public Trap trap;
    private Animator anim;
    string animationState = "AnimationState";
    private Vector3 pos;
    bool spent = false;

    void Start()
    {
        anim = GetComponent<Animator>();
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
    }

    IEnumerator MyCoroutine(Vector3 pos)
    {
        yield return new WaitForSeconds(2f);

        string name = trap.trapName;

        if(name == "ZeusMainTrap")
        {
            gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
            Debug.Log(pos);

            float x = pos.x;
            float y = pos.y;
            float z = pos.z;
            GameObject zeusSouthEast = (GameObject)Resources.Load("Zeus/ZeusSouthEast", typeof(GameObject));
            GameObject actualSouthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y - 1.5f, z), Quaternion.identity);
            GameObject actualSouthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y - 1.5f, z), Quaternion.Euler(new Vector3(0,0,-90)));
            GameObject actualNorthEast = Instantiate(zeusSouthEast, new Vector3(x + 1, y + 0.5f, z), Quaternion.Euler(new Vector3(0,0,90)));
            GameObject actualNorthWest = Instantiate(zeusSouthEast, new Vector3(x - 1, y + 0.5f, z), Quaternion.Euler(new Vector3(0,0,180)));
        }

        
        Debug.Log(trap.trapName);
        anim.SetInteger(animationState, 1);

        if(name == "PoseidonTrap" && spent == false)
        {
            spent = true;
            yield return new WaitForSeconds(0.1f);
            int num = UnityEngine.Random.Range(0,3);
            gameObject.transform.Rotate(0.0f, 0.0f, num * 90.0f, Space.Self);
        }
        

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
       
        yield return 0;
    }

}
