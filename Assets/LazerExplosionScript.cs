using UnityEngine;
using System.Collections;

public class LazerExplosionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("KillLazerExplosion");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator KillLazerExplosion()	{
		yield return new WaitForSeconds (this.GetComponent<ParticleSystem>().duration + .5f);
		Destroy (this.gameObject);
	}
}
