using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Numerics;

namespace GameServer {
    class ServerSend {
        private static void SendTCPData(int _toClient, Packet _packet) {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet) {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet) {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet) {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != _exceptClient) {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet) {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet) {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != _exceptClient) {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

#region Packets
        public static void Welcome(int _toClient, string _msg) {
            using (Packet _packet = new Packet((int)ServerPackets.welcome)) {
                _packet.Write(_msg);
                _packet.Write(_toClient);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player) {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer)) {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.charType);
		_packet.Write(_player.ready);
		//Console.Write("Player spawned");
                SendTCPData(_toClient, _packet);
            }
        }


	public static void PlayerPosition(Player _player){
	    using (Packet _packet = new Packet((int)ServerPackets.playerPosition)){
		_packet.Write(_player.id);
		_packet.Write(_player.movePosition);
		if(_player.poseidonMove == true)
		    _packet.Write(true);
		else
		    _packet.Write(false);
		Console.Write(_player.poseidonMove);
		if(_player.isAlive || _player.poseidonMove){
		    SendTCPDataToAll(_packet);
		    Console.Write("TRIED TO MOVE");
		}
	    }
	}

        public static void PlayerDisconnected(int _playerId) {
            using (Packet _packet = new Packet((int)ServerPackets.playerDisconnected)) {
                _packet.Write(_playerId);
                SendTCPDataToAll(_packet);
            }
        }

        public static void TimerInfo(ServerCountdownTimer _currentTime){
            using (Packet _packet = new Packet((int)ServerPackets.timer)) {
                _packet.Write(_currentTime.currentTime);
                _packet.Write(_currentTime.isZero);
                SendTCPDataToAll(_packet);
                //Console.WriteLine($"{_currentTime.currentTime} sent timer packet");
                //Console.WriteLine($"{_currentTime.isZero} is the isZero status");
            }
        }
        
	public static void ReadyFlag(Player _player){
	    using (Packet _packet = new Packet((int)ServerPackets.readyFlag)){
		_packet.Write(_player.id);
		_packet.Write(_player.ready);
		_packet.Write(_player.everyoneReady);
		_packet.Write(_player.startPressed);
		_packet.Write(Player.definitelyUseful);
		SendTCPDataToAll(_packet);
	    }
	}

	public static void sendTrap(int playerId, Vector3 pos, int trapId){
	    using (Packet _packet = new Packet((int)ServerPackets.trap)){
		_packet.Write(playerId);
		_packet.Write(pos);
		_packet.Write(trapId);
		SendTCPDataToAll(_packet);
	    }
	}
	
#endregion
    }
}
