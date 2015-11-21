using UnityEngine;
using System.Collections;

public class ObjectProperties : MonoBehaviour {

	Crane crane;
	public float weight;
	public bool conducting;
	public bool isPowerOn = false;
	public bool isStartPoint;
	public bool isEndPoint;
	// Use this for initialization
	void Start () {
		crane = GameObject.FindWithTag ("Crane").GetComponent<Crane> ();
		if (isStartPoint || isEndPoint) {
			SetRandomYPos();
		}
	}
	
	// Update is called once per frame
	void Update () {
	if (isEndPoint && isPowerOn) {
			StartCoroutine(wait (3.0f));
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	void FixedUpdate() {


		if (transform.position.y < 0.0f) {

			crane.score -= 1;
			Destroy(gameObject);

		}
	}
	void SetRandomYPos() {
		int rand = Random.Range (1, 6);
		transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y + (float)rand, transform.localPosition.z);
	}

	public void SetColor(Color color) {
		transform.GetComponent<Renderer> ().material.color = color;


	}

	void OnTriggerEnter(Collider col) {
		// start the conduct
		if(conducting) {
			try {
				ObjectProperties op = col.transform.GetComponent<ObjectProperties>();
				if(op.conducting && op.isPowerOn) {
					isPowerOn = true;
					SetColor (Color.yellow);
					Debug.Log ("OntriggerEnter - conducting mat touch!");
				}			
			} catch(System.NullReferenceException) {}
		
				

	   }
	}
	void OnTriggerStay(Collider col) {
		// don't really do anything? 
		if(conducting) {
			try {
				ObjectProperties op = col.transform.GetComponent<ObjectProperties>();
				if(op.conducting && op.isPowerOn) {
					isPowerOn = true;
					SetColor (Color.yellow);
					Debug.Log ("OntriggerEnter - conducting mat touch!");
				} else {
					//isPowerOn = false;
					//SetColor (Color.green);
				}
			} catch(System.NullReferenceException) {}
			
			
			
		}
	}
	void OnTriggerExit(Collider col) {
		// stop the conduct
		if(conducting) {
			//isPowerOn = false;
			//SetColor (Color.green);
		}
	}

	IEnumerator wait(float delay)
	{
		yield return new WaitForSeconds (delay);
	}
}
