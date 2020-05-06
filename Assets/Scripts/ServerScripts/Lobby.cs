using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour {

    public static Lobby instance;
    public string username;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Debug.Log("Instance already exists, detroying object");
            Destroy(this);
        }
	
	Debug.Log(MainMenu.name);
	//username = MainMenu.name;
	username = "test";
	StartCoroutine("connectToServer");
    }

    

    public void startGame(){
	GameManager.instance.inventoryCanvas.enabled = true;
	GameObject.Find("Lobby").SetActive(false);
        //CountdownTimer.instance.StartTimer();
    }

    public void startGameTimer()
    {
        GameObject.Find("Lobby").SetActive(false);
        CountdownTimer.instance.StartTimer();
    }

    IEnumerator connectToServer(){
	while(Client.instance == null){
	    yield return null;
	}
	Client.instance.ConnectToServer();
    }
}
