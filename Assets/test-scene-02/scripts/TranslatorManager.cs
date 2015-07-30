using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class TranslatorManager : MonoBehaviour {

	public GameObject cube;
	private List<GameObject> spectrumObjects = new List<GameObject> ();

	// world music variables
	public static float[] spectrumData = new float[256];
	public static float spectrumAvg;

	void Start () {
		for (int x = 0; x < spectrumData.Length; x++) {
			cube.transform.localScale = new Vector3 (1f, 1f, 3f);
			spectrumObjects.Add (Instantiate (cube, new Vector3 (-10+x, -1f, 0f), Quaternion.identity) as GameObject);
		}
	}
	

	void Update () {
		spectrumData = AudioListener.GetSpectrumData (spectrumData.Length, 0, FFTWindow.Rectangular);
		spectrumAvg = 0f;

		for (int i = 0; i < spectrumData.Length; i++) {
			spectrumObjects[i].transform.localScale = Vector3.Lerp (spectrumObjects[i].transform.localScale, new Vector3 (spectrumObjects[i].transform.localScale.x, spectrumData[i] * 5f * (i+2f), spectrumObjects[i].transform.localScale.z), 5f * Time.deltaTime);
			spectrumAvg += spectrumData[i];
		}

		spectrumAvg /= spectrumData.Length;

	}

}
