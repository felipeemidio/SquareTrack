using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadStage : MonoBehaviour {
	[SerializeField]
	private int stage;

	void Start()
	{
		Button btn = this.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	}

	void TaskOnClick()
	{
		SceneManager.LoadScene ("Stage_" + stage, LoadSceneMode.Single);
	}
}
