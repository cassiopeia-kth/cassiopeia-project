using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    float speed = 0.0001f;
    public Rigidbody2D rb;
    public Vector3 posInit;
    public int i = 0;
    public Collider cd;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	if(Input.GetKey(KeyCode.UpArrow) && i > 10){
	    rb.MovePosition(rb.position + new Vector2(0,1));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.DownArrow) && i > 10){
	    rb.MovePosition(rb.position - new Vector2(0,1));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.RightArrow) && i > 10){
	    rb.MovePosition(rb.position + new Vector2(1,0));
	    i = 0;
	}
	if(Input.GetKey(KeyCode.LeftArrow) && i > 10){
	    rb.MovePosition(rb.position - new Vector2(1,0));
	    i = 0;
	}
	i++;
	
	if(Input.GetKeyUp(KeyCode.UpArrow)){
	    i = 10;
	}
	if(Input.GetKeyUp(KeyCode.DownArrow)){
	    i = 10;
	}
	if(Input.GetKeyUp(KeyCode.RightArrow)){
	    i = 10;
	}
	if(Input.GetKeyUp(KeyCode.LeftArrow)){
	    i = 10;
	}	    
    }

    void OnCollisionEnter2D(Collision2D col){	
	if(col.gameObject.name == "Hole"){
	    Debug.Log("OnCollisionEnter2D");

	}
	    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	if(other.gameObject.name == "Hole"){
	    Debug.Log("OnCollisionEnter2D TRIGGER");

	}

    }

}
