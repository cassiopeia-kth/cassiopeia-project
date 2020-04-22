using UnityEngine;

public class TrapInteraction : MonoBehaviour 
{
    public Trap trap;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 pos = gameObject.transform.position;
        Debug.Log(gameObject.transform.position);
        pos.z = 0;
        gameObject.transform.position = pos;
        //gameObject.animator.controller.speed = 0;
    }

}
