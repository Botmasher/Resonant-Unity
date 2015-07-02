using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	void Awake () {
		StartCoroutine ("KillMe");
	}

	void Update () {
		transform.position += Vector3.right * 18f * Time.deltaTime;
	}

	IEnumerator KillMe () {
		yield return new WaitForSeconds (1.2f);
		Destroy (this.gameObject);
		yield return null;
	}

}
