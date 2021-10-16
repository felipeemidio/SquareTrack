using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggle : MonoBehaviour {

	private bool isOn;
	private MainController controller;

	void Start () {
		isOn = true;
		controller = GameObject.FindGameObjectWithTag ("MainController").GetComponent<MainController> ();
		//Debug.Log ("O botão está ligado? " + controller.GetMusic() );
		if (isOn != controller.GetMusic ())
			StartCoroutine(Foo ());
	}

	// Turn Off or Turn On the music state and change the button position
	private void ChangeState() {
		isOn = !isOn;
		if (isOn) {
			transform.localPosition = new Vector3 (0.0f, transform.localPosition.y, 0.0f);
		} else {
			transform.localPosition = new Vector3 (100.0f, transform.localPosition.y, 0.0f);
		}
	}

	// Active when the player press the music button.
	public void MusicChange () {
		ChangeState ();
		controller.SetMusic (isOn);
	}


	// I have to call a thread because transform changes can't be done when we are in the Start step of unity
	public IEnumerator Foo () {
		yield return new WaitForSeconds (1.0f);
		ChangeState ();
	}
}
