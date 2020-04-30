using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _packet.Write(UIManager.instance.usernameField.text);
            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs) {
	if(activateSleep){
	    timer -= Time.deltaTime;
	    if(timer <= 0){
		activateSleep = false;
	    }
	    else{
		return;
	    }
	}

        using (Packet _packet = new Packet((int)ClientPackets.playerMovement)) {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs) {
                _packet.Write(_input);
		//Debug.Log(_input);
            }
	    _packet.Write(GameManager.players[Client.instance.myId].isAlive);
	    _packet.Write(GameManager.players[Client.instance.myId].transform.position);
            SendUDPData(_packet);
	    //ActivateSleep(2.5f);
        }
    }
    #endregion
    
    public static void ActivateSleep(float forSeconds){
	timer = forSeconds;
	activateSleep = true;
    }

}
