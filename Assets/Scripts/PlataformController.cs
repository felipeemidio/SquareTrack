using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformController : MonoBehaviour {

	void OnTriggerExit(Collider other) {
		if(other.CompareTag("Player"))
			Destroy(gameObject, 0.3f);
	}
}
