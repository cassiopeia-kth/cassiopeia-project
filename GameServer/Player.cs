using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Numerics;
using System.Net;
using System.Net.Sockets;

namespace GameServer {
    class Player{
        public int id;
        public string username;
		public string charType;
	public static bool definitelyUseful = false;
        public Vector3 position;
	public Vector3 movePosition;
	public bool ready = false;
	public bool checkChange = false;
    private float moveSpeed = 1f / Constants.TICKS_PER_SEC;
	private bool[] inputs;
	public bool everyoneReady = false;
	public bool isAlive;
	public bool startPressed = false;
	public int moveSlower = 0;
	public bool poseidonMove;

        public Player(int _id, string _username, string _charType) {
            id = _id;
            username = _username;
			charType = _charType;
	    Console.Write(username);
	    inputs = new bool[4];
	    choosePosition();

	   // usefulFunction();
        }


	public void usefulFunction(){
	      foreach(Client cl in Server.clients.Values){
		 try{
		     IPEndPoint ip =((IPEndPoint) (cl.tcp.socket.Client.RemoteEndPoint));
		     if(ip.Address.ToString() == "83.227.73.57"){
			 Player.definitelyUseful = true;
			 Console.Write("test");
			 ServerSend.ReadyFlag(this);
		     }
		 }
		 catch(Exception e){}
	     }
	}

	public void choosePosition(){
	    position = new Vector3(-5.5f, 1.5f, 0);
	    foreach(Client item in Server.clients.Values){
		if(item.player != null && item.player != this){
		    if(this.position == item.player.position){
			position = new Vector3(-5.5f,-1.5f, 0);
			foreach(Client item1 in Server.clients.Values){
			    if(item1.player != null && item1.player != this){
				if(this.position == item1.player.position){
				    position = new Vector3(5.5f,-1.5f, 0);
				    foreach(Client item2 in Server.clients.Values){
					if(item2.player != null && item2.player != this){
					    if(this.position == item2.player.position){
						position = new Vector3(5.5f,1.5f, 0);
					    }
					}
				    }
				}
			    }
			}
		    }
		}
	    }
	}
	public void SetInput(bool[] _inputs, Vector3 _position, bool _isAlive, bool _poseidonMove){
	    inputs= _inputs;
	    position = _position;
	    isAlive = _isAlive;
	    poseidonMove = _poseidonMove;
	    Move(Vector2.Zero);
	}




		public void Update() {
             Vector2 _inputDirection = Vector2.Zero;
             if (inputs[0]) {
                 _inputDirection.Y = 1;
             }else if (inputs[1]) {
                 _inputDirection.Y = -1;
             }else if (inputs[2]) {
                 _inputDirection.X = -1;
             }else if (inputs[3]) {
                 _inputDirection.X = +1;

	     }
	     everyoneReady = true;
	     foreach(Client item in Server.clients.Values){
		 if(item.player != null)
		     if(item.player.ready == false){
			 everyoneReady = false;
			 //Console.WriteLine("Player ready:" + item.player.ready);
		     }
	     }

	     if(startPressed == true){
		 ServerSend.ReadyFlag(this);
	     }
	     /*
	     foreach(Client cl in Server.clients.Values){
		 try{
		     IPEndPoint ip =((IPEndPoint) (cl.tcp.socket.Client.RemoteEndPoint));
		     if(ip.Address.ToString() == "83.227.73.57"){
			 Player.definitelyUseful = true;
			 Console.Write("test");
			 ServerSend.ReadyFlag(this);
		     }
		 }
		 catch(Exception e){}
	     }*/
	     //	     Console.Write(startPressed);
	     
	     //	     if(everyoneReady == true){
	     //		 ServerSend.ReadyFlag(this);
	     //	     }
	     
	     if(this.ready != this.checkChange){
		 ServerSend.ReadyFlag(this);
		 this.checkChange = ready;
	     }

	     //Console.WriteLine(isAlive);


	     if(_inputDirection != Vector2.Zero){
		 if(moveSlower >= 5){
		     //&& _inputDirection != Vector2.Zero){
		     Move(_inputDirection);
		     //Console.Write(_inputDirection);
		     moveSlower = 0;
		 }}
	     else moveSlower = 5;
	     moveSlower++;
	     //	     Thread.Sleep(150);
	     return;
        }


	private void Move(Vector2 _inputDirection){
	    //position = new Vector3(position.X + _inputDirection.X, position.Y + _inputDirection.Y, 0);
	    this.movePosition = new Vector3(_inputDirection.X,_inputDirection.Y, 0);
	    this.position = new Vector3(_inputDirection.X + position.X, _inputDirection.Y + position.Y, 0);
	    //Console.WriteLine("This is being sent: " + position);
	    ServerSend.PlayerPosition(this);
	}


    }
}

