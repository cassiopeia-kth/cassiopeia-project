using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Trap trap;
    public AudioSource pickUpSound;
    private Animator anim;
    string animationState = "AnimationState";

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (anim.GetInteger(animationState) < 1)
        {
            StartCoroutine(MyCoroutine());
        }
    }

    IEnumerator MyCoroutine()
    {
        anim.SetInteger(animationState, 1);
        pickUpSound.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        yield return 0;
    }
}
