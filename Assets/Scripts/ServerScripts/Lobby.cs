using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Lobby : MonoBehaviour {

    public static Lobby instance;
    public string username;
    public GameObject notReadyButton;
    public GameObject readyButton;
    public GameObject startButton;
    public bool gameStarted = false;
    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Debug.Log("Instance already exists, detroying object");
            Destroy(this);
        }
	
	//Debug.Log(MainMenu.name);
	username = MainMenu.name;
	username = username.Remove(username.Length - 1);
	//username = "test";
	notReadyButton = GameObject.Find("NotReadyButton");
	readyButton = GameObject.Find("ReadyButton");
	startButton = GameObject.Find("StartGameButton");
	GameObject.Find("NotReadyButton").SetActive(false);
	startButton.SetActive(false);
	StartCoroutine("connectToServer");
    }

    public void Update(){
	if(GameManager.players.ContainsKey(Client.instance.myId))
	    if(GameManager.players[Client.instance.myId].everyoneReady == true){
		Debug.Log("Everyone ready");
	    }
	foreach(PlayerManager pman in GameManager.players.Values){
	    if(pman.isAlive == true){
		pman.isAlive = false;
	    }
	}

	foreach(PlayerManager id in GameManager.players.Values){
	    Lobby.instance.displayReadyorNot(id.id);
	}
	try{
	    Lobby.instance.displayReadyorNot(GameManager.players[Client.instance.myId].id);
	}
	catch(Exception e ){}
	//Debug.Log(GameManager.players[Client.instance.myId].id);
	//Debug.Log(GameManager.players[Client.instance.myId].isReady);
	
    }

    public void displayStartButton(){
	startButton.SetActive(true);
    }

    public void theMostUsefulFunction(){
	Debug.Log("GOD JOINED");
    }
    
    public void hideStartButton(){
	startButton.SetActive(false);
    }
    
    public void setReady(){
	GameManager.players[Client.instance.myId].isReady = true;
	GameObject.Find("ReadyButton").SetActive(false);
	//	GameObject.Find("NotReadyButton").SetActive(true);
	notReadyButton.SetActive(true);
    }


    public void setNotReady(){
	GameManager.players[Client.instance.myId].isReady = false;
	readyButton.SetActive(true);
	notReadyButton.SetActive(false);
    }

    public void startGame(){
	GameManager.players[Client.instance.myId].startPressed = true;
	GameManager.instance.roundCount = 0;
	ClientSend.ReadyFlag();
		//ClientSend.sendStartTimer();
		//if (CountdownTimer.instance.currentTime > 18)
		//{
			GameManager.instance.inventoryCanvas.enabled = true;
			this.gameStarted = true;
			foreach (PlayerManager pman in GameManager.players.Values)
			{
				pman.isAlive = true;
			}
			//GameObject.Find("Lobby").SetActive(false);
			Canvas a = gameObject.GetComponent<Canvas>();
			a.enabled = false;
			GameObject.Find("Lobby").SetActive(false);
			//CountdownTimer.instance.StartTimer();
		//}
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

    public void removeFromList(int _id){
	if(GameObject.Find("Username_Player_1").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
	    GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Username_Player_1").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Text_Name_Player_1").GetComponent<TextMeshProUGUI>().text = "Waiting for player...";
	    GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
	    GameManager.instance.nameList[0] = null;
	    return;
	}

	if(GameObject.Find("Username_Player_2").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
	    GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Username_Player_2").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Text_Name_Player_2").GetComponent<TextMeshProUGUI>().text = "Waiting for player...";
	    GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
	    GameManager.instance.nameList[1] = null;
	    return;
	}
	if(GameObject.Find("Username_Player_3").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
	    GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Username_Player_3").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Text_Name_Player_3").GetComponent<TextMeshProUGUI>().text = "Waiting for player...";
	    GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
	    GameManager.instance.nameList[2] = null;
	    return;
	}
	if(GameObject.Find("Username_Player_4").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
	    GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Username_Player_4").GetComponent<TextMeshProUGUI>().text = "";
	    GameObject.Find("Text_Name_Player_4").GetComponent<TextMeshProUGUI>().text = "Waiting for player...";
	    GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
	    GameManager.instance.nameList[3] = null;
	    return;
	}
	  
    }

    public void displayReadyorNot(int _id){
	if(GameManager.players[_id] != null)
	if(GameManager.players[_id].isReady == true){
	    if(GameObject.Find("Username_Player_1").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().text = "Ready";
		GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_2").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().text = "Ready";
		GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_3").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().text = "Ready";
		GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_4").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().text = "Ready";
		GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().color = new Color32(0,255,0,255);
		return;
	    }
	}
    
	if(GameManager.players[_id].isReady == false){
	    if(GameObject.Find("Username_Player_1").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().text = "Not Ready";
		GameObject.Find("Text_Ready_Player_1").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_2").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().text = "Not Ready";
		GameObject.Find("Text_Ready_Player_2").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_3").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().text = "Not Ready";
		GameObject.Find("Text_Ready_Player_3").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
		return;
	    }
	    if(GameObject.Find("Username_Player_4").GetComponent<TextMeshProUGUI>().text == GameManager.players[_id].username){
		GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().text = "Not Ready";
		GameObject.Find("Text_Ready_Player_4").GetComponent<TextMeshProUGUI>().color = new Color32(255,0,0,255);
		return;
	    }
	}
    }
    
}
