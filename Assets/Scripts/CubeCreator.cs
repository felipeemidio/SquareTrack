using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCreator : MonoBehaviour {

	[SerializeField]
	private List<GameObject> cubeList;

	public float t_max;
	public float t_min;


	void Start(){
		StartCoroutine (createCubes() );
	}

	IEnumerator createCubes(){

		while (true) {
			// Define the variable
			float waitTime = Random.Range(t_min, t_max);
			float height = Random.Range (transform.localPosition.y - 10, transform.localPosition.y + 10);
			// Reandomly select a place to create a cube in the x axis.
			Vector3 pos = new Vector3 (Random.Range (-transform.localScale.x, transform.localScale.x), 
				height, 
				gameObject.transform.position.z);

			// Instantiate one cube randomly.
			int cubeIndex = Random.Range (0, cubeList.Count);
			Instantiate (cubeList[cubeIndex] , pos, Quaternion.identity);

			yield return new WaitForSeconds (waitTime);
		}
	}
}
