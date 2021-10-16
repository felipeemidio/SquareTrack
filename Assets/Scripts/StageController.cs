using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

	[SerializeField]
	private List<Button> buttons_list;

	private MainController controller;
	private int currentStage;

	void Awake () {
		controller = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ();
		currentStage = controller.GetCurrentStage ();
	}

	void Start () {
		// Enable stages already cleared.
		Debug.Log ("current stage is " + currentStage);
		for (int i = 1; i <= buttons_list.Count; i++) {
			if (i < currentStage) {
				// Show trophy
				string childName = "Trophy";
				GameObject trophyImage = buttons_list [i - 1].transform.Find (childName).gameObject;
				trophyImage.SetActive (true);

				// Stop when there are no more button.
				if (i == buttons_list.Count)
					continue;

				// Activate the button.
				buttons_list [i].interactable = true;

				// The text should be lighter when the button isn't interactable.
				Text text = buttons_list [i].GetComponentInChildren<Text> ();
				Color currentColor = text.color;
				currentColor.a = 1.0f;
				text.color = currentColor;
			} else {
				// Desactivate the button.
				buttons_list [i].interactable = false;

				// The text should be darker when the button isn't interactable.
				Text text = buttons_list [i].GetComponentInChildren<Text> ();
				Color currentColor = text.color;
				currentColor.a = 0.5f;
				text.color = currentColor;
			}

		}

		/*
		// The text should be darker when the button isn't interactable.
		Color currentColor = text.color;
		if (btn.interactable) {
			currentColor.a = 1.0f;
			text.color = currentColor;

		} else {
			currentColor.a = 0.5f;
			text.color = currentColor;
		}
		*/
	}



}
