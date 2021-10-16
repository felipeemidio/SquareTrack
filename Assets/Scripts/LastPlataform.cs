using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlataform : MonoBehaviour {

	private GameController gameController;

	void Awake () {
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine( gameController.WinGame () );
	}
}
