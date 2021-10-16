using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongPlataform : MonoBehaviour {

	private bool strong;
	private Animator changeColor;
	private GameObject parent; 

	void Awake () {
		strong = true;
		parent = transform.parent.gameObject;
		changeColor = parent.GetComponent<Animator> ();
	}
	

	void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        if (strong) {	
			//Debug.Log ("exit the strong plataform");
			changeColor.SetTrigger ("PlayerOut");
			strong = false;
		} else {
			//Debug.Log ("destroy the strong plataform");
			Destroy(parent, 0.1f);
		}
	}
}
