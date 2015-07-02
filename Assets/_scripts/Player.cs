using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// behavior variables
	[Range(0,200)]public int baseSpeed;		// base movement speed
	[Range(0,100)]public int jumpHeight;	// base jump strength
	
	private float horiz;					// store x-axis input
	private float currentSpeed;				// extra speed plus base speed

	// control flow checks
	private bool isGrounded;				// has player made contact with floor and not left it?

	// projectile firing vars
	public GameObject bullet;				// projectile to spawn on fire
	private float bulletTime=0f;			// fires almost as fast as you can pull the trigger

	void Awake () {
		currentSpeed = baseSpeed;

		gameObject.GetComponent<Rigidbody> ().mass = 2f;
	}

	void Update () {
		
		horiz = Input.GetAxis ("Horizontal");
		
		GetComponent<Rigidbody>().AddForce (Vector3.right * (horiz*10f + baseSpeed * horiz * Time.deltaTime));

		if (Input.GetButtonDown("Jump") && isGrounded) {
			GetComponent<Rigidbody>().AddForce(Vector3.up*jumpHeight*currentSpeed);
		}

		// spawn bullet if ready and press fire. Bullet movement and destruction in bullet's script
		bulletTime += Time.deltaTime;
		if (Input.GetButtonDown("Fire1") && bulletTime >= 0.1f) {
			bulletTime = 0f;
			Instantiate (bullet, this.transform.position, this.transform.rotation);
		}

	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = true;
		}

		if (other.gameObject.tag == "Camera Box") {
			// lerp camera towards 
		}
	}

	void OnCollisionExit (Collision other) {
		if (other.gameObject.tag == "Ground") {
			isGrounded = false;
		}
	}

}
