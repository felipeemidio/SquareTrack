using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainController : MonoBehaviour {

	public static MainController control;
	private AudioSource audioSrc;
	private string music01;
	private string music02;
	private int currentStage;
	private bool musicOn;
	private bool soundOn;

	void Awake () {
		musicOn = true;
		soundOn = true;
		audioSrc = GetComponent<AudioSource> ();

		music01 = "Puzzle-Dreams-3";
		music02 = "376416__ehohnke__funk-lead-loop";

		// Make the MainController Undestructable.
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else {
			Destroy (gameObject);
		}

		//Instantiate the variables.
		currentStage = 1;

		#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		//Load if exist previous data
		Load ();

		#endif

		if(audioSrc != null)
			UpdateMusic ();
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// Called when a new scene is loaded.
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (!audioSrc.enabled)
			return;

		// The music depends of the current scene.
		if (scene.buildIndex >= 3) {
			if (audioSrc.clip == null || audioSrc.clip.name != music01) {
				audioSrc.clip = Resources.Load<AudioClip>("Sounds/" + music01);
				audioSrc.Play ();
			}
		} else {
			if (audioSrc.clip == null || audioSrc.clip.name != music02) {
				audioSrc.clip = Resources.Load<AudioClip>("Sounds/" + music02);
				audioSrc.Play ();
			}
		}
	}

	/*
	 * Get the numer of the current stage. 
	 */
	public int GetCurrentStage () {
		return currentStage;
	}

	/*
	 * Method that is called when a stage id cleared. 
	 */
	public void StageCleared(int id) {
		//Debug.Log ("You Cleared stage " + id.ToString());
		if (currentStage <= id) {
			currentStage = id + 1;
			//Debug.Log ("Your current stage is " + currentStage.ToString());
		
			#if UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			// Save the new currentStage variable.
			Save ();
			#endif	
		}


	}

	public void Save () {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/squareTrackData.dat");
		MyData data = new MyData ();
		data.cStage = currentStage;
		data.music = musicOn;
		data.sound = soundOn;
		bf.Serialize (file, data);
		file.Close ();

		//Debug.Log ("Save Sucessful");
	}

	public void Load () {
		if (File.Exists (Application.persistentDataPath + "/squareTrackData.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/squareTrackData.dat", FileMode.Open);

			MyData data = (MyData)bf.Deserialize (file);
			file.Close ();

			currentStage = data.cStage;
			musicOn = data.music;
			soundOn = data.sound;
			//Debug.Log ("currentStage " + currentStage.ToString());
			Debug.Log ("Load Sucessful");
		}
	}

	public void SetMusic( bool isOn) {
		musicOn = isOn;
		UpdateMusic ();
	}

	public void SetSound( bool isOn) {
		soundOn = isOn;
	}

	public bool GetMusic () {
		return musicOn;
	}

	public bool GetSound () {
		return soundOn;
	}

	private void UpdateMusic(){
		if (musicOn)
			audioSrc.enabled = true;
		else
			audioSrc.enabled = false;

		this.Save ();
	}

}

[Serializable]
class MyData {
	public int cStage;
	public bool music;
	public bool sound;
}
