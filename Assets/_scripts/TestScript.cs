using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestScript : MonoBehaviour {

	// inspector variables
	public GameObject cube;
	public GameObject dot;
	private List<GameObject> dots = new List<GameObject>();
	[Range(0,10)] public int cubesToSpawn = 1;

	// game cubes spawn and destroy variables
	private List<GameObject> mySpawnedCubes = new List<GameObject>();
	private float xAxis = -15f;
	private List<int> myRandoms = new List<int>();
	private int myRandom;

	// internal references to world objects
	private Light myLamp;
	private Camera myCam;
	private Rigidbody ball;

	// internal audio data variables;
	public static float[] audioSpectrum = new float[256];
	private float timeSinceBeat;
	private float bPM=0f;
	private bool isCountingBeats = true;


	void Awake () {

		// fill audio spectrum with initial values (silence)
		for (int i=0; i<audioSpectrum.Length; i++) {
			audioSpectrum[i]=0f;
			// spawn row of small cubes for equalizer display
			dots.Add(Instantiate(dot, new Vector3(xAxis,-3f,0f), Quaternion.identity) as GameObject);
			xAxis+=0.11f;
		}

		/* spawn number of cubes set in inspector
		xAxis = 0f;
		for (int i=0; i<cubesToSpawn; i++) {
			mySpawnedCubes.Add (Instantiate (cube, new Vector3(xAxis,0f,0f), Quaternion.identity) as GameObject);
			xAxis += 2f;
		}*/

		// find scene objects
		myLamp = GameObject.FindObjectOfType<Light>();
		myCam = GameObject.FindObjectOfType<Camera>();
	}

	void Update () {

		// get spectrum data from the audio listener (will pick up any changes to music as it's played)
		audioSpectrum = AudioListener.GetSpectrumData (audioSpectrum.Length, 0, FFTWindow.Hamming);
		//audioSpectrum = GetComponent<AudioSource>().GetSpectrumData (audioSpectrum.Length, 0, FFTWindow.Hamming);

		float myCount=0f;
		float myAvg=0f;
		// make row of small cubes behave like waveform display
		for (int i=0; i<audioSpectrum.Length; i++) {
			myAvg+=audioSpectrum[i];
			dots[i].transform.localScale = Vector3.Lerp
				(dots[i].transform.localScale, new Vector3(0.1f,(i*5f)*audioSpectrum[i],10f), 7f*Time.deltaTime);
			//dots[i].transform.localScale = Vector3.Lerp (dots[i].transform.localScale, new Vector3(0.1f,Mathf.Log(audioSpectrum[i])/5f,10f), 7f*Time.deltaTime);
			myCount++;
		}

		// rotate lamp around to simulate sunrise and set over time
		myLamp.transform.Rotate (Vector3.right*10f*Time.deltaTime);
	
	}


	public static float FindSpectrum () {
		float avg=0f;
		for (int i = 0; i < audioSpectrum.Length; i++) {
			avg += audioSpectrum[i];
		}
		return (avg / (float)audioSpectrum.Length);
	}








	/**
	 * 	Average together the total spectrum data
	 *		- arithmetic avg useful for e.g. making things generally bounce to music
	 */
	public static float AverageSpectrum () {
		float avg=0f;
		for (int i=0; i<audioSpectrum.Length; i++) {
			avg+=audioSpectrum[i];
		}
		return avg/audioSpectrum.Length;
	}


	// kill all cubes in list of cubes
	IEnumerator CubeKiller () {
		yield return new WaitForSeconds (5f);
		for (int i=0; i<mySpawnedCubes.Count; i++) {
			myRandom = (int)Mathf.Round(Random.Range(0, cubesToSpawn));
			while (myRandoms.Contains(myRandom) == true) {
				myRandom = (int)Mathf.Round(Random.Range(0, cubesToSpawn));
			}
			myRandoms.Add(myRandom);
			Destroy (mySpawnedCubes[myRandom]);
			yield return new WaitForSeconds(0.1f);
		}
	}

}
