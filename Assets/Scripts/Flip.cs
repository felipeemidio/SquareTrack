using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour {

	private GameController gameController;
	private bool hasSound;
	private AudioSource flipAudio;
	private Rigidbody rb;
	private Vector2 touchOrigin;
	private int lastMovement;
	private int n;
	private bool isFalling;
	private bool isFlipping;
	private AdsHandler adsHandler;
	private FollowPlayer follower;

	public float rotateTime;
	private float degree;
    private float rate;

	private bool rotating;
	// Use this for initialization
	void Start () {
		lastMovement = 0;
		n = 0;
		isFlipping = false;
		isFalling = false;
		touchOrigin = -Vector2.one;
		flipAudio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
        follower = GameObject.FindGameObjectWithTag("Follower").GetComponent<FollowPlayer> ();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController> ();
		hasSound = GameObject.FindGameObjectWithTag("MainController").GetComponent<MainController> ().GetSound ();
		adsHandler = GameObject.FindGameObjectWithTag("MainController").GetComponent<AdsHandler>();
		degree = 90;
        rate = degree / rotateTime;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isFalling) {
			return;
		}
		if (transform.position.y < -0.5f) {
			StartCoroutine ("DestroyPlayer");
		}
        if (isFlipping)
        {
            return;
        }

		//Check if we are running either in the Unity editor or in a standalone build.
		#if UNITY_STANDALONE || UNITY_WEBPLAYER

		if (Input.GetKeyDown (KeyCode.RightArrow) && !isFlipping) {
			isFlipping = true;
			StartCoroutine( "FlipRight" ) ;
		} else if (Input.GetKeyDown (KeyCode.LeftArrow) && !isFlipping ) {
			isFlipping = true;
			StartCoroutine( "FlipLeft" ) ;
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && !isFlipping ) {
			isFlipping = true;
			StartCoroutine( "FlipFoward" ) ;
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && !isFlipping ) {
			isFlipping = true;
			StartCoroutine( "FlipBackward" ) ;
		}

		//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

		//Check if Input has registered more than zero touches
		if (Input.touchCount > 0){

			//Store the first touch detected.
			Touch myTouch = Input.touches[0];

			//Check if the phase of that touch equals Began
			if (myTouch.phase == TouchPhase.Began)
			{
				//If so, set touchOrigin to the position of that touch
				touchOrigin = myTouch.position;
			}
			//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
			else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0 && !isFlipping)
			{
				//Set touchEnd to equal the position of this touch
				Vector2 touchEnd = myTouch.position;

				//Calculate the difference between the beginning and end of the touch on the x axis.
				float x = touchEnd.x - touchOrigin.x;

				//Calculate the difference between the beginning and end of the touch on the y axis.
				float y = touchEnd.y - touchOrigin.y;

				//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
				touchOrigin.x = -1;

				//This is for small touchs do not move the player.
				if(Mathf.Abs(x) < 5 && Mathf.Abs(y) < 5){
					return ;
				}

				//Check if the difference along the x axis is greater than the difference along the y axis.
				isFlipping = true;
				if (Mathf.Abs(x) > Mathf.Abs(y)){
					
					if (x > 0){
						
						StartCoroutine( "FlipRight" ) ;
					}
					else{
						StartCoroutine( "FlipLeft" ) ;
					}
				}
				else {
					if (y > 0){
						StartCoroutine( "FlipFoward" ) ;
					}
					else{
						StartCoroutine( "FlipBackward" ) ;
					}
				}
			}
		}
		#endif //End of mobile platform dependendent compilation section started above with #elif

	}

	public IEnumerator FlipFoward() {
		rb.isKinematic = true;
		rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		lastMovement = 1;
        
        Vector3 expectedRotation = transform.rotation.eulerAngles;
        Vector3 expectedPosition = transform.position;
        
        Vector3 edgePos = new Vector3 (transform.position.x , transform.position.y-0.5f, transform.position.z + 0.5f );

		float foo = 0.0f;
		float i = 0.0f;
		float spin = 0.0f;
		while (i < degree) {
			foo = i;
			transform.RotateAround (edgePos, Vector3.right, spin);
			spin = rate * Time.deltaTime; 
			i += spin;
			yield return null;

		}
		//Debug.Log ("foo: " + foo + " i " + i);
		transform.RotateAround (edgePos, Vector3.right, degree-foo);
        
        expectedRotation.x += 90.0f;
        transform.rotation = Quaternion.Euler(expectedRotation);
        
        expectedPosition.z += 1.0f;
        transform.position = expectedPosition;
        

        ResetVariables();
        
    }

	public IEnumerator FlipRight() {
		rb.isKinematic = true;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		lastMovement = 2;

        Vector3 expectedRotation = transform.rotation.eulerAngles;
        Vector3 expectedPosition = transform.position;

        Vector3 edgePos =  new Vector3 (transform.position.x + 0.5f, transform.position.y-0.5f, 0.0f);

		float foo = 0.0f;
		float i = 0.0f;
		float spin = 0.0f;
		while (i < degree) {
			foo = i;
			transform.RotateAround (edgePos,  Vector3.forward, -spin);
			spin = rate * Time.deltaTime; 
			i += spin;
			yield return null;
		}
		//Debug.Log ("foo: " + foo + " i " + i);
		transform.RotateAround (edgePos,  Vector3.forward, foo-degree);

        expectedRotation.z += 90.0f;
        transform.rotation = Quaternion.Euler(expectedRotation);

        expectedPosition.x += 1.0f;
        transform.position = expectedPosition;

        ResetVariables();
    }
		
	public IEnumerator FlipBackward() {
		rb.isKinematic = true;
		rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		lastMovement = 3;

        Vector3 expectedRotation = transform.rotation.eulerAngles;
        Vector3 expectedPosition = transform.position;

        Vector3 edgePos = new Vector3 (transform.position.x , transform.position.y-0.5f, transform.position.z - 0.5f );

		float foo = 0.0f;
		float i = 0.0f;
		float spin = 0.0f;
		while (i < degree) {
			foo = i;
			transform.RotateAround (edgePos,  Vector3.right, -spin);
			spin = rate * Time.deltaTime; 
			i += spin;
			yield return null;
		}
		//Debug.Log ("foo: " + foo + " i " + i);
		transform.RotateAround (edgePos,  Vector3.right, foo-degree);

        expectedRotation.x -= 90.0f;
        transform.rotation = Quaternion.Euler(expectedRotation);

        expectedPosition.z -= 1.0f;
        transform.position = expectedPosition;

        ResetVariables();
    }

	public IEnumerator FlipLeft() {
		rb.isKinematic = true;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
		lastMovement = 4;

        Vector3 expectedRotation = transform.rotation.eulerAngles;
        Vector3 expectedPosition = transform.position;

        Vector3 edgePos = new Vector3 (transform.position.x - 0.5f, transform.position.y-0.5f, 0.0f);

		float foo = 0.0f;
		float i = 0.0f;
		float spin = 0.0f;
		while (i < degree) {
			foo = i;
			transform.RotateAround (edgePos,  Vector3.forward, spin);
			spin = rate * Time.deltaTime; 
			i += spin;
			yield return null;
		}
		
		transform.RotateAround (edgePos,   Vector3.forward, degree-foo);

        expectedRotation.z -= 90.0f;
        transform.rotation = Quaternion.Euler(expectedRotation);

        expectedPosition.x -= 1.0f;
        transform.position = expectedPosition;

        ResetVariables ();
    }


	IEnumerator DestroyPlayer () {
		isFalling = true;
		// Wait 1.5 sec's for destroy the player.
		yield return new WaitForSeconds (1.5f);
		adsHandler.ShowDefaultAd ();
		gameController.LoseGame ();
		Destroy (gameObject);
	}

    //Reset the variables after a flip and play the sound.
    private void ResetVariables()
    {
        if (flipAudio != null && n > 0 && hasSound)
            flipAudio.Play();
        rb.isKinematic = false;
        //yield return new WaitForSeconds (0.1f);
        rb.constraints = RigidbodyConstraints.None;
        if (follower.SomethingBelow())
        {
            isFlipping = false;
        }
    }

	public int GetLastMovement () {
		return lastMovement;
	}

	public void SetIsFlipping (bool boolValue) {
		isFlipping = boolValue;
	}

	void OnTriggerEnter (Collider other) {
		n = n + 1;
	}
	void OnTriggerExit (Collider other) {
		n = n - 1;
	}

}
