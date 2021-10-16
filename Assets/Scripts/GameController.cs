using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
	private Flip playerScript;
	private GameObject stage;
	private GameObject winWindow;
	private GameObject loseWindow;
	private AudioSource clickSound;
	private MainController controller;
	private int ID;
	private bool hasSound;

	private Animator animWin;
	private Animator animLose;

	void Start () {

		controller = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ();
		hasSound = controller.GetSound ();
		clickSound = GetComponent<AudioSource> ();

		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<Flip>();
		stage = GameObject.FindGameObjectWithTag("Stage");

		// Get the ID from the scene's name.
		string stageName = SceneManager.GetActiveScene ().name;
		int.TryParse (stageName.Substring(6) , out ID);

		winWindow = GameObject.FindGameObjectWithTag ("WinWindow");
		loseWindow = GameObject.FindGameObjectWithTag ("LoseWindow");

		animWin = winWindow.GetComponent<Animator> ();
		animLose = loseWindow.GetComponent<Animator> ();

		winWindow.SetActive (false);
		loseWindow.SetActive (false);
	}


	public IEnumerator WinGame(){
		yield return new WaitForSeconds (0.3f);
		// Count if the pre requisite to win was reached.
		int breakableObjects = 0;
		int childs = stage.transform.childCount;
		//Debug.Log ("childs = " + childs.ToString ());
		for (int i = 0; i < childs; i++) {
			if (stage.transform.GetChild (i).CompareTag ("Breakable")) {
				breakableObjects++;
			}
		}


		if (breakableObjects <= 0) {
			winWindow.SetActive (true);
			if (hasSound) {
				winWindow.GetComponent<AudioSource> ().PlayDelayed (1.0f);
			}
			if (!Application.CanStreamedLevelBeLoaded ("Stage_" + (ID + 1))) {
				Button nextButton = GameObject.FindGameObjectWithTag ("NextButton").GetComponent<Button> ();
				nextButton.interactable = false;
			}
			animWin.SetTrigger ("PlayerWin");
			playerScript.enabled = false;
			//Debug.Log ("  Win.");
			controller.StageCleared ( ID );

		}
	}

	public void LoseGame(){
		loseWindow.SetActive (true);
		if (hasSound)
			loseWindow.GetComponent<AudioSource> ().PlayDelayed (1.0f);
		animLose.SetTrigger ("PlayerLose");
		//Debug.Log ("  Not win.");
	}

	public void NextStage() {
		if (clickSound != null && hasSound) {
			clickSound.Play ();
		}
		//Debug.Log ("Loading Stage_" + (ID + 1).ToString ());
		SceneManager.LoadScene ("Stage_" + (ID + 1).ToString (), LoadSceneMode.Single);
	}

	public void BackToStages (){
		if (clickSound != null && hasSound) {
			clickSound.Play ();
		}
		//Debug.Log ("Stage Pressed!");
		SceneManager.LoadScene ("StageSelection", LoadSceneMode.Single);
	}

	public void ReloadScene (){
		if (clickSound != null && hasSound) {
			clickSound.Play ();
		}
		Scene scene = SceneManager.GetActiveScene(); 
		SceneManager.LoadScene(scene.name);
	}
}
