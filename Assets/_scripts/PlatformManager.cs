using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class PlatformManager : MonoBehaviour {

	// platform properties
	[Range(0f,20f)]public float speed=1.5f;
	[Range(0,800)]public int spinSpeed;
	public bool spinning;
	public bool horizontalSliding;
	public bool verticalSliding;
	[Range(0,20)]public float spinFrequency=2f;		// how often should it spin?

	// control flow for actions and timing
	private float cooldown=0f;			// has enough time passed to behave again? 
	private float spin=0f;				// current spin along axis
	private Vector3 startPos;			// starting position checker (treated as behavior "origin")
	private float xDir=1f;				// horizontal direction to travel
	private float yDir=1f;				// vertical direction to travel

	private GameObject cubesey;

	/*
	// test vars for ontrigger music interaction
	public AudioMixer mixer;
	private bool isTriggered=false;
	private bool mixing=false;
	private float tempo;*/

	void Awake () {
		cubesey = GameObject.Find ("Cube");
	}

	// Update is called once per frame
	void Update () {
		cooldown += Time.deltaTime;
		if (cooldown >= spinFrequency && spinning) {
			Spin();
		}

		if (horizontalSliding || verticalSliding) {
			Slide();
		}

		transform.localScale = new Vector3 (5f+(15f*TestScript.AverageSpectrum()), 0.4f+(15f*TestScript.AverageSpectrum()), 4f+(15f*TestScript.AverageSpectrum()));

		/*mixer.GetFloat ("tempo", out tempo);

		if (tempo > 1.05f && this.name == "Platform 2") {
			spinning = true;
		}

		if (mixing) {
			Debug.Log ("I'm mixing!");
			StartCoroutine(UpTempo(speed));
		}*/
	}
	/*
	IEnumerator UpTempo (float percent) {
		// store current tempo
		float initTempo;
		mixer.GetFloat("tempo", out initTempo);
		// increase tempo
		mixer.SetFloat ("tempo", initTempo*percent);
		mixing = false;
		// return downtempo
		yield return new WaitForSeconds (5f);
		mixer.SetFloat ("tempo", initTempo);
	}*/

	// slide right-left or up-down
	private void Slide() {

		for (int i=0; i<=TestScript.audioSpectrum.Length; i++) {
			// spectrum behavior
		}

		if (horizontalSliding) {
			if (transform.position.x <= startPos.x-3f) {
				xDir = 1f;
			} else if (transform.position.x >= startPos.x+3f) {
				xDir = -1f;
			}
			transform.Translate (Vector3.right * xDir * speed * Time.deltaTime);
		}

		if (verticalSliding) {
			if (transform.position.y <= startPos.y-3f) {
				yDir = 1f;
			} else if (transform.position.y >= startPos.y+3f) {
				yDir = -1f;
			}
			transform.Translate (Vector3.up * yDir * speed * Time.deltaTime);
		}

	}


	// spin on z axis
	private void Spin() {

		// rotate-y towards 360
		if (spin < 360f) {
			spin += spinSpeed*Time.deltaTime;
			transform.Rotate (new Vector3 (0f, 0f, spinSpeed*Time.deltaTime));
			//transform.rotation = Quaternion.Euler (Vector3.forward*spinSpeed*Time.deltaTime);

		// reset rot-y, rot-y checker and cooldown for next spin
		} else {
			spin = 0f;
			cooldown = 0f;
			transform.rotation = Quaternion.Euler (new Vector3(0f,0f,0f));
		}

	}


	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Player" && this.name == "Platform Horizontal 1") {
			cubesey.GetComponent<Animator> ().SetBool ("isUpping", true); 
		}
	}

	void OnCollisionExit (Collision other) {
		if (other.gameObject.tag == "Player" && this.name == "Platform Horizontal 1") {
			cubesey.GetComponent<Animator> ().SetBool ("isUpping", false); 
		}
	}

}
