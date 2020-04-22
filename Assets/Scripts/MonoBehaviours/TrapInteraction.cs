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
        anim = GetComponent<Animator>();
        currentAnim = GetComponent<Animation>();
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

        //anim.SetInteger(animationState, 1);

        StartCoroutine(MyCoroutine());

        anim.SetFloat("animspeed", 1f);
        Debug.Log(anim.speed);

    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(2f);

        if(trap.trapName == "ZeusMainTrap")
        {
           // SortingLayer slayer = gameObject.GetComponent<SortingLayer>();
            //slayer = "Zeus";
            gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
        }

        anim.SetInteger(animationState, 1);

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
       
        yield return 0;
    }

}
