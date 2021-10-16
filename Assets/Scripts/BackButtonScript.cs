using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonScript : MonoBehaviour {

	public void BackButton() {
		//Debug.Log ("Back Pressed!");
		AudioSource audio = GetComponent<AudioSource> ();
		bool hasSound = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ().GetSound ();
		if(audio != null && hasSound)
			audio.Play ();
		SceneManager.LoadScene ("StartPanel", LoadSceneMode.Single);
	}
}
