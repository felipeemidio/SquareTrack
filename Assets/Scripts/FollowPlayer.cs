using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	
	Transform playerT;
	int count;

	// Use this for initialization
	void Start () {
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerT = player.transform;
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (playerT != null) {
			Vector3 newPos = new Vector3 (playerT.position.x, transform.position.y, playerT.position.z);
			if(newPos != transform.position)
				transform.position = newPos;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Breakable") || other.CompareTag("LastPlataform")) {
			count--;
			//Debug.Log ("exit: " + other.name);
		}
			
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Breakable") || other.CompareTag("LastPlataform")) {
			count++;
            //Debug.Log("enter: " + other.name);
        }
	}

    public bool SomethingBelow(){
		if (count > 0) {
			return true;
		} else {
			return false;
		}
	}
}
