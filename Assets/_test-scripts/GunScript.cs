using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour {

	// locating player to follow as its gun
	private Transform playerPosition;

	// shooting objects
	public GameObject lazer;
	public GameObject lazeplosion;

	// store raycast ray and data
	private RaycastHit hit;
	private Ray ray = new Ray();
	private GameObject explosion;


	// Use this for initialization
	void Start () {
		playerPosition = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (playerPosition.position.x + 1f, playerPosition.position.y, 0f);
		this.transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, 0f));

		// shoot lazers
		if (Input.GetButtonDown ("Fire2")) {
			//raycast
			ray.origin = transform.position;
			ray.direction = Vector3.right;
			if (Physics.Raycast (ray, out hit)) {
				StartCoroutine("ShootExplode");
				Debug.Log ("I hit "+hit.transform.name+"!");
			}
			Debug.DrawRay(ray.origin, ray.direction*10000f, Color.red);
		}

	}


	// instantiate explosion object, wait, destroy explosion to manage memory
	IEnumerator ShootExplode() {
		Instantiate (lazer, this.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (0.02f);
		Instantiate (lazeplosion, hit.transform.position, Quaternion.identity);
		yield return new WaitForSeconds (1f);
	}


}
