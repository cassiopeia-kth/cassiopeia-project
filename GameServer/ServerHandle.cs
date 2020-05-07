using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer {
    class ServerHandle {
        public static void WelcomeReceived(int _fromClient, Packet _packet) {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();
	    _username = _username.Remove(_username.Length - 1);
	    Console.WriteLine(_username);
            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck) {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
        }


	public static void PlayerMovement(int _fromClient, Packet _packet){
	    bool[] _inputs = new bool[_packet.ReadInt()];
	    for(int i = 0; i < _inputs.Length; i++){
		_inputs[i] = _packet.ReadBool();
	    }
	    bool isAlive = _packet.ReadBool();
	    Vector3 _position = _packet.ReadVector3();
//	    Console.WriteLine(_position);
	    try{
		Server.clients[_fromClient].player.SetInput(_inputs, _position, isAlive);
	    }
	    catch(Exception e){
		Console.Write(e);
	    }
	}

	public static void PlayerReady(int _fromClient, Packet _packet){
	    bool isReady = _packet.ReadBool();
	    bool startPressed = _packet.ReadBool();
	    Console.Write(isReady);
	    try{
		Server.clients[_fromClient].player.ready = isReady;
		Server.clients[_fromClient].player.startPressed = startPressed;
	    }
	    catch(Exception e){
		Console.Write(e);
	    }
	}
	
    }

    
}
