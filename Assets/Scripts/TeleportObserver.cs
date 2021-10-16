using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObserver : MonoBehaviour {

	Teleport tA;
	void Awake(){
		tA = gameObject.transform.parent.GetComponent<Teleport> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine( tA.Activate (other.gameObject) );
		}
	}
}
