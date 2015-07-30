using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndZone : MonoBehaviour {

	IEnumerator FadeOutRestart () {
		yield return new WaitForSeconds (5f);
		Application.LoadLevel (Application.loadedLevel);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			GetComponent<AudioSource> ().Play ();
			StartCoroutine ("FadeOutRestart");
		}
	}

}
