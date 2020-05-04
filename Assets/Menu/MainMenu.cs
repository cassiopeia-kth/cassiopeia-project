using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour {

    public string name;
    
    public void PlayGame () 
    {
	name = GameObject.Find("UsernameInput").GetComponent<TextMeshProUGUI>().text;
	SceneManager.LoadScene("Level1_ch95");
    }
   
	public void QuitGame ()
    {
	Debug.Log ("QUIT!");
	Application.Quit();
    }
  
}
