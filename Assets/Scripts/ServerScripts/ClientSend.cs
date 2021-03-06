﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Diagnostics;

public class ClientSend : MonoBehaviour {
    private static bool activateSleep = false;
    private static float timer;
    private static void SendTCPData(Packet _packet) {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet) {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived() {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived)) {
            _packet.Write(Client.instance.myId);
            _packet.Write(Lobby.instance.username);
            //Char TYPE
	    _packet.Write(MainMenu.charType);
//            Debug.Log(MainMenu.charType);

//	    Debug.Log(Lobby.instance.username);
            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs) {

        using (Packet _packet = new Packet((int)ClientPackets.playerMovement)) {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs) {
                _packet.Write(_input);
            }
	    _packet.Write(GameManager.players[Client.instance.myId].isAlive);
	    _packet.Write(GameManager.players[Client.instance.myId].transform.position);
	    _packet.Write(GameManager.players[Client.instance.myId].poseidonMove);
            SendTCPData(_packet);
        }
    }


    public static void ReadyFlag(){
	using (Packet _packet = new Packet((int)ClientPackets.readyFlag)) {
	    _packet.Write(GameManager.players[Client.instance.myId].isReady);
	    _packet.Write(GameManager.players[Client.instance.myId].startPressed);
	    //Debug.Log(GameManager.players[Client.instance.myId].startPressed);

	    SendTCPData(_packet);
	}
	
    }

    public static void sendStartTimer(){
	using (Packet _packet = new Packet((int)ClientPackets.timer)) {
	    _packet.Write(GameManager.players[Client.instance.myId].startPressed);
	    //	SendTCPData(_packet);
	}
    }
    
    #endregion
    public static void sendTrap(int id, Vector3 pos, int trapId){
	using (Packet _packet = new Packet((int)ClientPackets.trap)) {
	    _packet.Write(id);
	    _packet.Write(pos);
	    _packet.Write(trapId);
	    SendTCPData(_packet);
	}
    }

	
    public static void ActivateSleep(float forSeconds){
	timer = forSeconds;
	activateSleep = true;
    }
    

}
