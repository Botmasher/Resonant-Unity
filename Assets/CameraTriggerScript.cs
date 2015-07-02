using UnityEngine;
using System.Collections;

public class CameraTriggerScript : MonoBehaviour {

	private Camera cam;


	void Start () {
		cam = Camera.main;
	}


	void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "Player") {
			// focus camera on player and center vertically to this box
			cam.transform.position = Vector3.Lerp (cam.transform.position, new Vector3 (other.transform.position.x, this.transform.position.y, cam.transform.position.z), Time.deltaTime);
		}
	}
}
