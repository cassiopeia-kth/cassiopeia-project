using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float timer;
    private bool activateSleep = false;

    private void FixedUpdate(){
	if(activateSleep)
	{
	    timer -= Time.deltaTime;
	    if(timer <= 0){
		activateSleep = false;
	    }
	    else{
		return;
	    }
	}

	SendInputToServer();
//	ActivateSleep(0.25f);
    }
    
    public void ActivateSleep(float forSeconds)
    {
	timer = forSeconds;
	activateSleep = true;
    }
    

    private void SendInputToServer(){
	bool[] _inputs = new bool[]{
	    Input.GetKey(KeyCode.W),
	    Input.GetKey(KeyCode.S),
	    Input.GetKey(KeyCode.A),
	    Input.GetKey(KeyCode.D),
	};
	ClientSend.PlayerMovement(_inputs);
    }
}
