using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusDiagonal : MonoBehaviour
{
    public Trap trap;
    private Animator anim;
    private Vector3 pos;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the lightning collides with a player, then change its sorting layer.
        Debug.Log("Hit my with your rhythm stick");
        gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
    }

    void Start()
    {
        // Start the lightning off at zero speed, and invisible.
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("animspeed", 0f);
        pos = gameObject.transform.position;
        pos.z = -100;
        gameObject.transform.position = pos;
    }

    void Update()
    {
        // As soon as it is ready, start a coroutine for its animation.
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        // Wait 1 second, so that the lightning coincides with the main lightning bolt which spawned it.
        yield return new WaitForSeconds(1f);
        // Start the animation's speed.
        anim.SetFloat("animspeed", 1f);
        // Make the animation visible.
        pos = gameObject.transform.position;
        pos.z = 0;
        gameObject.transform.position = pos;
        // Wait for two seconds, then delete the object.
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return 0;
    }
    

    
}
