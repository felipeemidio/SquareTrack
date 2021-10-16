using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartPageController : MonoBehaviour {

	private MainController controller;
	private AudioSource audioSrc;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ();
		audioSrc = GetComponent<AudioSource> ();
	}


	public void StartGame() { 
		if (audioSrc != null && controller.GetSound())
			audioSrc.Play ();

		int stage = controller.GetCurrentStage ();
		// If the stage do not exist, open the previous one. 
		if (!Application.CanStreamedLevelBeLoaded("Stage_" + stage)) {
			stage -= 1;
		}

		SceneManager.LoadScene ("Stage_" + stage, LoadSceneMode.Single);
	}

	public void StageScene() {
		//Debug.Log ("Stage Pressed!");
		if (audioSrc != null && controller.GetSound())
			audioSrc.Play ();
		
		SceneManager.LoadScene ("StageSelection", LoadSceneMode.Single);
	}

	public void ExitGame() {
		controller.Save ();
		//Debug.Log ("Exit Pressed!");
		if (audioSrc != null && controller.GetSound())
			audioSrc.Play ();
		Application.Quit ();
	}

	public void CreditsGame() {
		//Debug.Log ("Credits Pressed!");
		SceneManager.LoadScene ("Credits", LoadSceneMode.Single);
	}

}
