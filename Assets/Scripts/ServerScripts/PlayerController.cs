using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float timer;
    private bool activateSleep = false;

    private void FixedUpdate(){
	SendInputToServer();
    }
    
    private void SendEmpty(){
	bool[] _inputs = new bool[]{
	    false,false,false,false
	};
	ClientSend.PlayerMovement(_inputs);

    }
    
    private void SendInputToServer(){
        StartCoroutine("timerFix");
    }

    IEnumerator timerFix(){
        while(CountdownTimer.instance == null){
            yield return null;
        }
        bool[] _inputs = new bool[4];
        if (CountdownTimer.instance.isZero)
        {
            ClientSend.PlayerMovement(_inputs);
        }
        else
        {
            _inputs = new bool[]{
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
            };

            ClientSend.PlayerMovement(_inputs);
        }
    } 
}
