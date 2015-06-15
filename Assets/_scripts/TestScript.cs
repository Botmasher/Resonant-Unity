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

	// input vars
	private float horiz;

	// internal references to world objects
	private Light myLamp;
	private Camera myCam;
	private Rigidbody ball;

	// internal audio data variables;
	private float[] audioSpectrum = new float[256];
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
		ball = GameObject.Find ("Ball").GetComponent<Rigidbody>();
	}

	void Update () {

		audioSpectrum = GetComponent<AudioSource>().GetSpectrumData (audioSpectrum.Length, 0, FFTWindow.Rectangular);

		horiz = Input.GetAxis ("Horizontal");

		ball.AddForceAtPosition (new Vector3(32f*horiz*Time.deltaTime,0f,0f), new Vector3(ball.transform.position.x*horiz+0.3f,0f,0f));

		// make row of small cubes behave like waveform display
		for (int i=0; i<audioSpectrum.Length; i++) {
			dots[i].transform.localScale = Vector3.Lerp (dots[i].transform.localScale, new Vector3(0.1f,(i*5f)*audioSpectrum[i],10f), 7f*Time.deltaTime);
		}

		/* attempt at beat detection
		if (//reaches beat slash freq peak) {
			if (isCountingBeats) {
				bPM = Mathf.Clamp (60f/timeSinceBeat, 10f, 400f);
				timeSinceBeat = 0f;
				isCountingBeats = false;
			} else {
				timeSinceBeat += Time.deltaTime;
			}
		} else {
			isCountingBeats = true;
			timeSinceBeat += Time.deltaTime;
		}
		print (bPM);

		if (bPM <= 90f) {
			myCam.backgroundColor = Color.Lerp (myCam.backgroundColor, Color.white, Time.deltaTime);
		} else {
			myCam.backgroundColor = Color.Lerp (myCam.backgroundColor, Color.gray, Time.deltaTime);
		}
		*/

		// rotate lamp around to simulate sunrise and set over time
		myLamp.transform.Rotate (Vector3.right*10f*Time.deltaTime);
	
	}

	// kill all cubes in list of cubes
	IEnumerator CubeKiller () {
		yield return new WaitForSeconds (5f);
		for (int i=0; i<mySpawnedCubes.Count; i++) {
			Debug.Log (mySpawnedCubes.Count);
			Debug.Log (mySpawnedCubes[myRandom]);
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
