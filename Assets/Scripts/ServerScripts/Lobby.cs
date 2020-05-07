using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
	
	Debug.Log(MainMenu.name);
	username = MainMenu.name;
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
    }

    public void displayStartButton(){
	startButton.SetActive(true);
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
	ClientSend.ReadyFlag();
	GameManager.instance.inventoryCanvas.enabled = true;
	GameObject.Find("Lobby").SetActive(false);
	this.gameStarted = true;
    }

    IEnumerator connectToServer(){
	while(Client.instance == null){
	    yield return null;
	}
	Client.instance.ConnectToServer();
    }

    public void displayReadyorNot(int _id){
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
