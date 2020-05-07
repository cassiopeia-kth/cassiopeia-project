using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Net;
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
        GameManager.instance.SpawnPlayer(_id, _username, _position);
	Debug.Log("did try to spawn the player");
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

	if(GameManager.players[_id].GetComponent<MovePlayer>() != null)
	    GameManager.players[_id].GetComponent<MovePlayer>().movePlayer(_position);
	else if(GameManager.players[_id].GetComponent<MovePlayerOnline>() != null)
	    GameManager.players[_id].GetComponent<MovePlayerOnline>().movePlayer(_position);
	
	Vector3 actual_position = GameManager.players[_id].transform.position;
//	GameManager.players[_id].transform.position = _position;	
    }

    public static void readyFlag(Packet _packet){
	//TODO make start button active
	int _id = _packet.ReadInt();
	bool isReady = _packet.ReadBool();
	bool everyoneReady = _packet.ReadBool();
	bool startPressed = _packet.ReadBool();
	GameManager.players[_id].isReady = isReady;
	Debug.Log(isReady);
	if(everyoneReady == true){
	    Lobby.instance.displayStartButton();
	}
	if(everyoneReady == false){
	    Lobby.instance.hideStartButton();
	}
	if(startPressed == true){
	    Lobby.instance.startGame();
	}
	
	Lobby.instance.displayReadyorNot(_id);
    }

    public static void ClientTimer(Packet _packet){
        int currentTime = _packet.ReadInt();
        Debug.Log($"{currentTime} is the current time");
        CountdownTimer.instance.currentTime = currentTime;
        CountdownTimer.instance.UpdateTimer();
    }
    
}
