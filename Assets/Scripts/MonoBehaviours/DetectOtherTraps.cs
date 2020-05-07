using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOtherTraps : MonoBehaviour
{
    public float initialisationTime;

    private void Start()
    {
        // initialisation time will be used to determine which trap to destroy.
        initialisationTime = Time.timeSinceLevelLoad;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If a trap collides with another trap, they must overlap. Delete the older one.
        if (collision.gameObject.tag == "TrapCollision")
        {
            if (initialisationTime - collision.gameObject.GetComponent<DetectOtherTraps>().initialisationTime < 0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
