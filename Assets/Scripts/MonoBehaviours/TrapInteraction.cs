using UnityEngine;
using System.Collections;

public class TrapInteraction : MonoBehaviour 
{
    public Trap trap;
    private Animator anim;
    private Animation currentAnim;
    string animationState = "AnimationState";

    void Start()
    {
        anim = GetComponent<Animator>();
        currentAnim = GetComponent<Animation>();
        anim.SetFloat("animspeed", 0f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = gameObject.transform.position;
        Debug.Log(gameObject.transform.position);
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
