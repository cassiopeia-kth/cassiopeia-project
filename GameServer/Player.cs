using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Numerics;

namespace GameServer {
    class Player {
        public int id;
        public string username;

        public Vector3 position;
        private float moveSpeed = 1f / Constants.TICKS_PER_SEC;
	private bool[] inputs;

	public bool isAlive;

        public Player(int _id, string _username, Vector3 _spawnPosition) {
            id = _id;
            username = _username;
            position = _spawnPosition;
	    inputs = new bool[4];
        }


	public void SetInput(bool[] _inputs, Vector3 _position, bool _isAlive){
	    inputs= _inputs;
	    position = _position;
	    isAlive = _isAlive;
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

	     Move(_inputDirection);
//	     Thread.Sleep(150);
	     return;
        }


	private void Move(Vector2 _inputDirection){
	 //   position = new Vector3(position.X + _inputDirection.X, position.Y + _inputDirection.Y, 0);
	    position = new Vector3(_inputDirection.X,_inputDirection.Y, 0);
	    //Console.Write(position);
	    ServerSend.PlayerPosition(this);
	}


    }
}
