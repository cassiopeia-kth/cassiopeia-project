using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
	
    }

    // Update is called once per frame
    void Update()
    {
	i++;
	if(i > 5){
	    float speed = 1f;
	    if(Input.GetKey(KeyCode.UpArrow)){
		transform.position += new Vector3(0, 1) * speed;
	    }
	    if(Input.GetKey(KeyCode.DownArrow)){
		transform.position += new Vector3(0, -1) * speed;
	    }
	    if(Input.GetKey(KeyCode.RightArrow)){
		transform.position += new Vector3(1, 0) * speed;
	    }
	    if(Input.GetKey(KeyCode.LeftArrow)){
		transform.position += new Vector3(-1, 0) * speed;
	    }
	    i = 0;
	}
    }
}
