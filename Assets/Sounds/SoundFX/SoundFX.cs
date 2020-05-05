using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public AudioSource Blip;
    public AudioSource Bupp;
    public AudioSource MenuOver;
    public AudioSource ChestOpening;

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

}
