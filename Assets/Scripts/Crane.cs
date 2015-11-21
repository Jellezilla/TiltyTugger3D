using UnityEngine;
using System.Collections;

public class Crane : MonoBehaviour {

	public float laneWidth;
	public GameObject CubePrefab;
	public GameObject CubePrefab2;
	bool isNextObjConducting;
	public float score;
	public float levelCompleted = 25;
	// Use this for initialization
	void Start () {
		score = 0.0f;
		laneWidth = 0.075F;
		isNextObjConducting = DetermineNextDropType ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.A)) {
			transform.localPosition = new Vector3(transform.localPosition.x - laneWidth, transform.localPosition.y, transform.localPosition.z);
			if(transform.localPosition.x < -0.375f) {

				transform.localPosition = new Vector3(-0.375f, transform.localPosition.y, transform.localPosition.z);
			}
		}
		if(Input.GetKeyDown (KeyCode.D)) {
			transform.localPosition = new Vector3(transform.localPosition.x + laneWidth, transform.localPosition.y, transform.localPosition.z);
			if(transform.localPosition.x > 0.375f) {
				transform.localPosition = new Vector3(0.375f, transform.localPosition.y, transform.localPosition.z);
			}
		}
		if(Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
		if(Input.GetKeyDown (KeyCode.Space)) {
			SpawnCube();
		}
	}

	bool DetermineNextDropType() {
		int rand = Random.Range(1,4);
		Debug.Log ("rand: " + rand);
		if (rand == 1) {
			return true;
		} else {
			return false;
		}

	}

	void SpawnCube() {
		Vector3 pos = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);

		if(isNextObjConducting) {
			Instantiate (CubePrefab, pos, Quaternion.identity);
		}
		else if (!isNextObjConducting) {
			Instantiate (CubePrefab2, pos, Quaternion.identity);
		}
		score++;
		isNextObjConducting = DetermineNextDropType ();
		//Vector3 pos = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
		//Instantiate (CubePrefab, pos, Quaternion.identity);

	}
	void OnGUI() {
		string obj = "";
		if (isNextObjConducting) {
			obj = "Green";
			//GUI.Label (new Rect (15, 15, 150, 25), "Next drop is going to be: " + obj);
		} else {
			obj = "Red";

		}
	//	GUI.Label (new Rect (15, 15, 150, 25), "Score: "+(int)score+" / "+levelCompleted);
		GUI.Label (new Rect (15, 15, 200, 25), "Next drop is going to be: " + obj);
	}
}
