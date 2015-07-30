using UnityEngine;
using System.Collections;

public class LazerScript : MonoBehaviour {

	void Awake() {
		StartCoroutine ("KillMe");
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * 40f);
	}

	IEnumerator KillMe () {
		yield return new WaitForSeconds (0.5f);
		Destroy (this.gameObject);
		yield return null;
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag != "Player" && other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast")) {
			Destroy (this.gameObject);
		}
	}
}
