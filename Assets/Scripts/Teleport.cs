using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

	[SerializeField]
	private Transform otherTeleport;
	private static bool isTeleporting;

	void Awake() {
		isTeleporting = false;
	}

	public IEnumerator Activate(GameObject player) {
		if (!isTeleporting) {
			isTeleporting = true;

			Transform playerT = player.transform;
			Rigidbody rb = player.GetComponent<Rigidbody> ();
			Flip playerScrip = player.GetComponent<Flip> ();
			Animator anim = player.GetComponent<Animator> ();

			// Prevents the player to move while teleporting.
			playerScrip.enabled = false;
			// Prevents the player to fall because of the scaling.
			rb.useGravity = false;

			// Play the animation to reduce the cube.
			anim.SetBool ("ScaleAnim", true);
			yield return new WaitForSeconds (1.0f);
			while (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
				yield return new WaitForSeconds (0.1f);
			}
			while (anim.GetCurrentAnimatorStateInfo (0).IsName ("ScaleMinimize")) {
				yield return new WaitForSeconds (0.1f);
			}
			anim.SetBool ("ScaleAnim", false);

			// Teleport the player.
			playerT.position = new Vector3 (otherTeleport.position.x, playerT.position.y, otherTeleport.position.z);
			//yield return new WaitForSeconds (0.5f);
			// Play the animation to increase the cube scale.
			anim.SetBool ("ScaleAnim2", true);
			yield return new WaitForSeconds (1.0f);
			while (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle2")) {
				yield return new WaitForSeconds (0.1f);
			}
			while (anim.GetCurrentAnimatorStateInfo (0).IsName ("ScaleMaximize")) {
				yield return new WaitForSeconds (0.1f);
			}
			anim.SetBool ("ScaleAnim2", false);

			rb.useGravity = true;
			playerScrip.enabled = true;

			//Flip the player to the last direction made.
			switch (playerScrip.GetLastMovement ()) {
			case 1:
				StartCoroutine (playerScrip.FlipFoward ());
				break;
			case 2:
				StartCoroutine (playerScrip.FlipRight ());
				break;
			case 3:
				StartCoroutine (playerScrip.FlipBackward ());
				break;
			case 4:
				StartCoroutine (playerScrip.FlipLeft ());
				break;	
			}

			isTeleporting = false;
		}

	}
		
}
