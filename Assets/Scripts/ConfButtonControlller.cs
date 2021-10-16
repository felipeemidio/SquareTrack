using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfButtonControlller : MonoBehaviour {

	Animator anim;

	void Awake(){
		anim = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<Animator> ();
	}

	public void ActiveTriggerConf(){
		anim.SetTrigger ("Conf");

	}

	public void ActiveTriggerStart(){
		anim.SetTrigger ("Start");

	}
}
