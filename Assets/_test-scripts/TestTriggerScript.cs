using UnityEngine;
using System.Collections;

public class TestTriggerScript : MonoBehaviour {

	private Light gameLight;
	private Color gameLightColor;

	void Awake () {
		// grab scene light, tagged helpfully by Jessica!
		gameLight = GameObject.FindGameObjectWithTag("LightSpeed").GetComponent<Light>();
	}

	void OnTriggerStay (Collider hitObject) {
		// player inside changes color to the beat
		if (hitObject.gameObject.tag == "Player") {

			// 
			if (TestScript.FindSpectrum()*1000f > 0.9f) {
				hitObject.GetComponent<MeshRenderer>().material.color = Color.Lerp (hitObject.GetComponent<MeshRenderer>().material.color, Color.magenta, Time.deltaTime);
			} else {
				hitObject.GetComponent<MeshRenderer>().material.color = Color.Lerp (hitObject.GetComponent<MeshRenderer>().material.color, Color.green, Time.deltaTime);
			}

			// lerp between colors based on whether rounded spectrum data is even/odd
			/*
			if ((int)(TestScript.FindSpectrum()*100000f) % 2 == 0) {
				hitObject.GetComponent<MeshRenderer>().material.color = Color.Lerp (hitObject.GetComponent<MeshRenderer>().material.color, Color.magenta, Time.deltaTime);
			} else {
				hitObject.GetComponent<MeshRenderer>().material.color = Color.Lerp (hitObject.GetComponent<MeshRenderer>().material.color, Color.green, Time.deltaTime);
			}
			*/

			// grow ever larger in strange patterns - try switching x/y/z vals
			//hitObject.transform.localScale += new Vector3(TestScript.FindSpectrum()*10f,0f,TestScript.FindSpectrum()*10f);
			//hitObject.transform.localScale = Vector3.Lerp(hitObject.transform.localScale, new Vector3(TestScript.FindSpectrum()*1000f, TestScript.FindSpectrum()*500f, 1f), 2f*Time.deltaTime);
		}
	}

}
