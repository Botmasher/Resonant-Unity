using UnityEngine;
using System.Collections;

public class KillTriggerScript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			StartCoroutine ("EndGame");
			//END GAME 
		}
	}

	IEnumerator	EndGame() {
		yield return new WaitForSeconds (3f);
		Application.LoadLevel(Application.loadedLevel);
	}
}
