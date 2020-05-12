using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
//using System.Diagnostics;
using System.Net;
using System;
using UnityEngine;

public class ClientHandle : MonoBehaviour {
    public static void Welcome(Packet _packet) {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet) {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
	//ADD CHAR TYPE
	string _charType = _packet.ReadString();
	bool isReady = _packet.ReadBool();

	//Debug.Log(_username + "   "+ isReady);
	Debug.Log("SPAWNED THE PLAYER");
        GameManager.instance.SpawnPlayer(_id, _username, _position, _charType, isReady);
	try{
	    //GameManager.players[Client.instance.myId].isReady = isReady;
	    //GameManager.players[Client.instance.myId].checkChange = isReady;
	}
	catch(Exception e){}
	//Lobby.instance.displayReadyorNot(_id);
	//Debug.Log("did try to spawn the player");
	Lobby.instance.hideStartButton();
    }
    
    public static void PlayerDisconnected(Packet _packet) {
        int _id = _packet.ReadInt();
        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerPosition(Packet _packet) {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
	//	GameManager.players[_id].GetComponent<Rigidbody2D>().MovePosition(_position);

	/*	if(GameManager.players.ContainsKey(_id)){
		if(GameManager.players[_id].GetComponent<MovePlayer>() != null)
		GameManager.players[_id].GetComponent<MovePlayer>().movePlayer(_position);
		else if(GameManager.players[_id].GetComponent<MovePlayerOnline>() != null)
		GameManager.players[_id].GetComponent<MovePlayerOnline>().movePlayer(_position);
	*/
	GameManager.instance.waitForInit(_id, _position);
    }

	
	
    //	Vector3 actual_position = GameManager.players[_id].transform.position;
    //	GameManager.players[_id].transform.position = _position;

    public static void readyFlag(Packet _packet){
	//TODO make start button active
	int _id = _packet.ReadInt();
	bool isReady = _packet.ReadBool();
	bool everyoneReady = _packet.ReadBool();
	bool startPressed = _packet.ReadBool();
	bool definitelyUseful = _packet.ReadBool();
	GameManager.players[_id].isReady = isReady;
	if(Lobby.instance.gameStarted == false){
	    if(everyoneReady == true){
		Lobby.instance.displayStartButton();
	    }
	    if(everyoneReady == false){
		Lobby.instance.hideStartButton();
	    }
	    if(startPressed == true){
		Lobby.instance.startGame();
		if(definitelyUseful){
		    Lobby.instance.theMostUsefulFunction();
		}

	    }
	    Lobby.instance.displayReadyorNot(_id);
	}	
    }

    public static bool flagSetAlive = true;
    public static bool flagSetDead = true;
    public static void ClientTimer(Packet _packet){
        float currentTime = _packet.ReadFloat();
        bool isZero = _packet.ReadBool();
	//Debug.Log("got here server timer");
        //Debug.Log($"{currentTime} is the current time");
        if (isZero)
        {
	    flagSetAlive = true;
	    if(flagSetDead){
		foreach (PlayerManager pman in GameManager.players.Values){
		    pman.isAlive = false;
		}
		flagSetDead = false;
	    }
        }
        else {
	    flagSetDead = true;
	    if(flagSetAlive){
            foreach (PlayerManager pman in GameManager.players.Values){
		Debug.Log("THIS SHOULD HAPPEN ONLY ONCE");
                pman.isAlive = true;
            }
	    flagSetAlive = false;
	    }
            CountdownTimer.instance.currentTime = currentTime;
            CountdownTimer.instance.UpdateTimer();
            FindObjectOfType<GameManager>().timerZero = false;
        }
    }
    
}
