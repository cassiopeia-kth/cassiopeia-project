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
        public Quaternion rotation;

        private float moveSpeed = 1f / Constants.TICKS_PER_SEC;
	private bool[] inputs;

	public bool isAlive;

        public Player(int _id, string _username, Vector3 _spawnPosition) {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = Quaternion.Identity;

	    inputs = new bool[4];
        }


	public void SetInput(bool[] _inputs, Quaternion _rotation, Vector3 _position, bool _isAlive){
	    inputs= _inputs;
	    rotation= _rotation;
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

//	     Console.Write(_inputDirection);
	     Move(_inputDirection);
	     Thread.Sleep(150);
	     return;
        }


	private void Move(Vector2 _inputDirection){
	    //Vector3 _forward = Vector3.Transform(new Vector3(0,0,0), rotation);
	    //Vector3 _right = Vector3.Normalize(Vector3.Cross(_forward, new Vector3(0,1,0)));

	    //Vector3 _moveDirection = _right * _inputDirection.X + _forward * _inputDirection.Y;
	    position = new Vector3(position.X + _inputDirection.X, position.Y + _inputDirection.Y, 0);

	    ServerSend.PlayerPosition(this);
	    ServerSend.PlayerRotation(this);
	}


    }
}
