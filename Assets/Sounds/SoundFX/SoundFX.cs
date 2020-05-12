using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource Blip;
    public AudioSource Bupp;
    public AudioSource MenuOver;
    public AudioSource ChestOpening;
    public AudioSource CharSelectSound; //put it here as it didnt work in the char_select script (pehaps because it goes out of the thing?)

    public void PlayBlip() {

    	Blip.Play();
    }

    public void PlayBupp() {

    	Bupp.Play();
    }

    public void PlayMenuOver() {

    	MenuOver.Play();
    }

	public void PlayChestOpening() {

    	ChestOpening.Play();
    }

    public void PlayCharSelectSound() {
    	CharSelectSound.Play();
    }

}
