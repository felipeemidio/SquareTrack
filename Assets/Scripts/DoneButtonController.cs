using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButtonController : MonoBehaviour {

	private Animator anim;
	private AudioSource clickSound;

	void Awake(){
		anim = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Animator> ();
		clickSound = GetComponent<AudioSource> ();
	}

	public void ActiveTriggerStart(){
		bool hasSound = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ().GetSound ();
		if (clickSound != null && hasSound)
			clickSound.Play ();
		anim.SetTrigger ("Start");

	}
}
