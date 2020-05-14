using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{

    public Vector3 StartPosition;
    public Vector3 EndPosition;
    private bool startAgain = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (startAgain)
        {
            gameObject.transform.position = StartPosition;
            startAgain = false;
        }
        
        if (gameObject.transform.position != EndPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, EndPosition, 120f * Time.deltaTime);
        }
    }

    public void ResetStartAgain()
    {
        startAgain = true;
    }
}
