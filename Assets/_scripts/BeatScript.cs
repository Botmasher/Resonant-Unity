using UnityEngine;
using System.Collections;

public class BeatScript : MonoBehaviour {

	private AudioSource song;			// song analyzed for beat data
	public GameObject cube;				// test cube to visualize beat data triggering action

	// multibeat oscillation checks
	private bool grooveRight=true;		// runs on multibeat to allow for back-forth actions
	private int beatCounter=0;
	private bool newBeat=false;

	float[] historyBuffer = new float[43];
	
	// Use this for initialization
	void Start () {
		song = GameObject.Find ("Level Manager").GetComponent<AudioSource> ();
		cube.transform.localScale = new Vector3(2f,2f,2f);
	}
	
	// Update is called once per frame
	void Update () {
		
		//compute instant sound energy
		float[] channelRight = song.GetSpectrumData (1024, 1, FFTWindow.Hamming);
		float[] channelLeft = song.GetSpectrumData (1024, 2, FFTWindow.Hamming);
		
		float e = sumStereo (channelLeft, channelRight);
		
		//compute local average sound evergy
		float E = sumLocalEnergy ()/historyBuffer.Length; // E being the average local sound energy
		
		//calculate variance
		float sumV = 0;
		for (int i = 0; i< 43; i++) 
			sumV += (historyBuffer[i]-E)*(historyBuffer[i]-E);
		
		float V = sumV/historyBuffer.Length;
		float constant = (float)((-0.0025714 * V) + 1.5142857);
		
		float[] shiftingHistoryBuffer = new float[historyBuffer.Length]; // make a new array and copy all the values to it
		
		for (int i = 0; i<(historyBuffer.Length-1); i++) { // now we shift the array one slot to the right
			shiftingHistoryBuffer[i+1] = historyBuffer[i]; // and fill the empty slot with the new instant sound energy
		}
		
		shiftingHistoryBuffer [0] = e;
		
		for (int i = 0; i<historyBuffer.Length; i++) {
			historyBuffer[i] = shiftingHistoryBuffer[i]; //then we return the values to the original array
		}
		
		//constant = 1.5f;
		
		if (e > (constant * E)) { // now we check if we have a beat
			cube.transform.localScale = Vector3.Lerp(cube.transform.localScale, new Vector3(2f, 50f*e, 2f), 2f*Time.deltaTime);

			/**
			 * added beat-peak and multibeat behavior
			 */
			if (newBeat) {
				beatCounter++;
				newBeat = false;
			}
			if (beatCounter > 0) {
				beatCounter = 0;
				if (grooveRight) {
					cube.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, 45f)), 3f*Time.deltaTime);
				} else {
					cube.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0f, 0f, -45f)), 3f*Time.deltaTime);
				}
			}
			// end added for multibeat

		} else {
			cube.transform.localScale = Vector3.Lerp(cube.transform.localScale, cube.transform.localScale*0.9f, 2f*Time.deltaTime);
			// test code for multibeat groove
			grooveRight = !grooveRight;
			newBeat = true;
		}

		/*
		Debug.Log ("Avg local: " + E);
		Debug.Log ("Instant: " + e);
		Debug.Log ("History Buffer: " + historybuffer());
		
		Debug.Log ("sum Variance: " + sumV);
		Debug.Log ("Variance: " + V);
		
		Debug.Log ("Constant: " + constant);
		Debug.Log ("--------");
		*/

	}


	float sumStereo(float[] channel1, float[] channel2) {
		float e = 0;
		for (int i = 0; i<channel1.Length; i++) {
			e += ((channel1[i]*channel1[i]) + (channel2[i]*channel2[i]));
		}
		
		return e;
	}
	
	float sumLocalEnergy() {
		float E = 0;
		
		for (int i = 0; i<historyBuffer.Length; i++) {
			E += historyBuffer[i]*historyBuffer[i];
		}
		
		return E;
	}
	
	string historybuffer() {
		string s = "";
		for (int i = 0; i<historyBuffer.Length; i++) {
			s += (historyBuffer[i] + ",");
		}
		return s;
	}

}