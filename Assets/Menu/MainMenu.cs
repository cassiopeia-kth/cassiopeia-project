using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {

    public static string name;
    
    public void PlayGame () 
    {
	SceneManager.LoadScene("Level1_ch95");
    }

    public void setUsername(){
	name = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
	Debug.Log(name);
	//GameObject test = GameObject.Find("UsernameInput");
	//Debug.Log(test);
    }
   
	public void QuitGame ()
    {
	Debug.Log ("QUIT!");
	Application.Quit();
    }
  
}
