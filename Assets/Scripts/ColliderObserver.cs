using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObserver : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		// Destroy the collider and the plataform.
		Destroy ( gameObject.transform.parent.gameObject, 0.1f );

		//Destroy ( gameObject );
	}
}
