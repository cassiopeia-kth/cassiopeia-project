using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {

    public string name;
    
    public void PlayGame () 
    {
	SceneManager.LoadScene("Level1_ch95");
    }

    public void setUsername(){
	name = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
    }
   
	public void QuitGame ()
    {
	Debug.Log ("QUIT!");
	Application.Quit();
    }
  
}
