using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour {

	private bool isOn;
	private MainController controller;

	void Start () {
		isOn = true;
		controller = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ();
		if (isOn != controller.GetSound ())
			StartCoroutine (Foo ());

	}

	// Turn Off or Turn On the sound state and change the button position
	private void ChangeState() {
		isOn = !isOn;
		if (isOn)
			transform.localPosition = new Vector3(0f, transform.localPosition.y, 0f);
		else
			transform.localPosition = new Vector3(100f, transform.localPosition.y, 0f);
	}

	// Active when the player press the sound button.
	public void SoundChange () {
		ChangeState ();
		controller.SetSound (isOn);
	}

	// I have to call a thread because transform changes can't be done when we are in the Start step of unity
	public IEnumerator Foo () {
		yield return new WaitForSeconds (1.0f);
		ChangeState ();
	}
}
