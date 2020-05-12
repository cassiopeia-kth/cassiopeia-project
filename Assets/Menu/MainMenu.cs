using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {

    public static string name;
    public static string charType;
    
    public void PlayGame () 
    {
	SceneManager.LoadScene("TimerGang");
    }

    public void playLobby1(){
	Client.port = 25850;
	SceneManager.LoadScene("TimerGang");
    }

    
    public void playLobby2(){
	Client.port = 25851;
	SceneManager.LoadScene("TimerGang");
    }

    
    public void playLobby3(){
	Client.port = 25852;
	SceneManager.LoadScene("TimerGang");
    }

    
    public void playLobby4(){
	Client.port = 25853;
	SceneManager.LoadScene("TimerGang");
    }

    public void setUsername(){
	name = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
//	Debug.Log(name);
	if(charType == null)
	    charType = "bishop";
	//GameObject test = GameObject.Find("UsernameInput");
	//Debug.Log(test);
    }

    public void setCharType(string _charType){
        charType = _charType;
    }

   
	public void QuitGame ()
    {
	Debug.Log ("QUIT!");
	Application.Quit();
    }
  
}
