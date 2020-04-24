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
        Debug.Log("Hit my with your rhythm stick");
        gameObject.GetComponent<Renderer>().sortingLayerName = "Zeus";
    }

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetFloat("animspeed", 0f);
        pos = gameObject.transform.position;
        pos.z = -100;
        gameObject.transform.position = pos;
    }

    void Update()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        anim.SetFloat("animspeed", 1f);
        pos = gameObject.transform.position;
        pos.z = 0;
        gameObject.transform.position = pos;
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return 0;
    }
    

    
}
