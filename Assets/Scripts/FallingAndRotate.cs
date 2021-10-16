using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAndRotate : MonoBehaviour {

	public float maxSpeed;
	public float minSpeed;
	public float speedRotate;

	private float speed;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		speed = Random.Range (minSpeed, maxSpeed);
		rb = GetComponent<Rigidbody> ();

		Vector3 dir = Vector3.back * speed * Time.deltaTime * 100;
		rb.AddForce ( dir );

		rb.angularVelocity = Random.insideUnitSphere * speedRotate;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < -18) {
			Destroy (gameObject);
		}
	}
}
