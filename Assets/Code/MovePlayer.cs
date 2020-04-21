using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    float speed = 5f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	if(Input.GetKey(KeyCode.UpArrow)){
	    Move(0,1);
	}
	if(Input.GetKey(KeyCode.DownArrow)){
	    Move(0,-1);
	}
	if(Input.GetKey(KeyCode.RightArrow)){
	    Move(1,0);
	}
	if(Input.GetKey(KeyCode.LeftArrow)){
	    Move(-1,0);
	}

	if(Input.GetKeyUp(KeyCode.UpArrow)){
	    Move(0,0);
	}
	if(Input.GetKeyUp(KeyCode.DownArrow)){
	    Move(0,0);
	}
	if(Input.GetKeyUp(KeyCode.RightArrow)){
	    Move(0,0);
	}
	if(Input.GetKeyUp(KeyCode.LeftArrow)){
	    Move(0,0);
	}

    }

    private void Move(int x, int y){
	rb.velocity = new Vector2(x,y).normalized * speed;
	
    }
}
