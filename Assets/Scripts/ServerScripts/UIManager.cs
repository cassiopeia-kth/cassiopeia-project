using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public GameObject startMenu;
    public InputField usernameField;

    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else if (instance != this){
            Debug.Log("Instance already exists, detroying object");
            Destroy(this);
        }
	Debug.Log(MainMenu.name);
	startMenu.SetActive(false);
	usernameField.interactable = false;
	usernameField.text = MainMenu.name;
	StartCoroutine("connectToServer");
    }

    public void ConnectToServer(){

	
    }

    IEnumerator connectToServer(){
	while(Client.instance == null){
	    yield return null;
	}
	Client.instance.ConnectToServer();
    }
}
