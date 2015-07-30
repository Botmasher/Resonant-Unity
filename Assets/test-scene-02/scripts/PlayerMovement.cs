using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	// control flow
	float blinkCounter = 0f;		// count up to next blink
	float blinkInterval;			// bounded random time between blinks
	bool isBlinking = false;
	bool cameraFollow = false;


	void Start () {
		// set time until first blink
		blinkInterval = Random.Range (0.2f, 5f);
	}


	void Update () {

		// blink if it's time
		if (blinkCounter >= blinkInterval && !isBlinking) {
			StartCoroutine ("Blink");
		} else {
			blinkCounter += Time.deltaTime;
		}

		// move LR
		GetComponent<Rigidbody>().AddForce (Vector3.right * Input.GetAxis("Horizontal") * 500f * Time.deltaTime);

		// bounce with beat if there's enough of one
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (1f + 100f * TranslatorManager.spectrumAvg, 1f + 100f * TranslatorManager.spectrumAvg, 1f + 2f * TranslatorManager.spectrumAvg), 5f * Time.deltaTime);

		if (cameraFollow) {
			Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, new Vector3 (transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z), 2f * Time.deltaTime);
		}

	}


	// do a quick blink
	IEnumerator Blink () {

		isBlinking = true;
		int i = 0;

		// count to full eye blink
		while (i < 90) {
			GetComponentInChildren<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, i);
			i++;
		}

		yield return new WaitForSeconds (0.08f);

		// count to full eye open
		while (i > 0) {
			GetComponentInChildren<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, i);
			i--;
		}

		// prepare for next blink
		isBlinking = false;
		blinkInterval = Random.Range (0.2f, 13f);
		blinkCounter = 0f;

	}

	// once enter camera box, camera starts following you
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "CameraTrigger") {
			cameraFollow = true;
		}
	}

}
