using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {

	// adjusting music settings
	public UnityEngine.Audio.AudioMixer mixer;
	private float previousValue;

	// control flow for trigger
	public bool pitchUp = false;
	private bool isPickedUp = false;


	// pitch down
	IEnumerator DownTempo () {

		mixer.GetFloat ("tempo", out previousValue);
		mixer.SetFloat ("tempo", previousValue / 2f);
		yield return new WaitForSeconds (5f);
		mixer.SetFloat ("tempo", previousValue);

		// reset pickup
		isPickedUp = false;
		yield return null;
	}


	// pitch up
	IEnumerator UpTempo () {

		mixer.GetFloat ("tempo", out previousValue);
		mixer.SetFloat ("tempo", previousValue * 2f);
		yield return new WaitForSeconds (5f);
		mixer.SetFloat ("tempo", previousValue);

		// reset pickup
		isPickedUp = false;
		yield return null;
	}


	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player" && !isPickedUp) {
			// set this branch inactive; pickup only picked up once!
			isPickedUp = true;

			// determine pickup's action
			if (pitchUp) {
				StartCoroutine ("UpTempo");
			} else {
				StartCoroutine ("DownTempo");
			}
		}
	}

}
