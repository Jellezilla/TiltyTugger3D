using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour {
	
	public GameObject Cube1;

	private Rigidbody rigid;

	public enum CraneType { speedDrop, craneForce };
	public CraneType craneType;
	float maxUpAndDown = 0.05F;             // amount of meters going up and down
	float speed = 100.0F;
	float maxSpeed = 20.0F;
	private float angle = 0.0F;
	private bool active;
	public float score = 0.0f;
	public int completedLevel = 25;

	GameObject player;

	public float dropDelay = 1.5f;
	private bool dropReady;
	private float toDegrees = Mathf.PI / 180;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
	
		score = 0.0f;
		rigid = transform.GetComponent<Rigidbody> ();
		rigid.useGravity = false;
		active = true;
		StartCoroutine (SpawnCall (5.0F));
		dropReady = false;
		StartCoroutine (SetDropReady (dropDelay));
	}
	
	// Update is called once per frame
	void Update () {


		if (craneType == CraneType.craneForce) {
			if (Input.GetKey (KeyCode.A)) {
//				rigid.AddForce (-Vector3.right * (speed / 3), ForceMode.Force);
				transform.position = new Vector3(transform.position.x - 0.051f, transform.position.y, transform.position.z);
			}
			if (Input.GetKey (KeyCode.D)) {
//				rigid.AddForce (Vector3.right * (speed / 3), ForceMode.Force);
				transform.position = new Vector3(transform.position.x +0.051f, transform.position.y, transform.position.z);
			}
		}
		if (craneType == CraneType.speedDrop) {
			if (Input.GetKeyDown (KeyCode.J)) {
				Debug.Log ("dropReady: "+dropReady);
				if(dropReady) {
				SpawnNewObject ();	

				}
			}
		}

	}
	void FixedUpdate() {

		
		if (score >= completedLevel) {
			WinLevel();
		}

		#region SpeedDrop
		if (active && craneType == CraneType.speedDrop)
		{
			angle += speed * Time.deltaTime;
			if (angle > 360) angle -= 360;
			transform.position = new Vector3(transform.position.x + (maxUpAndDown * Mathf.Cos(angle * toDegrees)), transform.position.y, transform.position.z);
		}
		#endregion

		#region craneForce
		if (active && craneType == CraneType.craneForce)
		{
			angle += speed * Time.deltaTime;
			if (angle > 360) angle -= 360;
			transform.position = new Vector3(transform.position.x + (maxUpAndDown * Mathf.Cos(angle * toDegrees)), transform.position.y, transform.position.z);

		}
		#endregion
		// restrictions
		if(rigid.velocity.magnitude > maxSpeed)
		{
			rigid.velocity = rigid.velocity.normalized * maxSpeed;
		}
		if (transform.position.x < -1.43f) 
			transform.position = new Vector3 (-1.42f, transform.position.y, transform.position.z);
		if (transform.position.x > 1.43f) 
			transform.position = new Vector3 (1.42f, transform.position.y, transform.position.z);

		if (player.transform.position.y < 0.0F) {
			LoseLevel();
		}
	}

	void SpawnNewObject() {
		
		
		score++;
		Vector3 pos = new Vector3 (transform.position.x, 5.0F, 0.0F);
		//(Instantiate(Cube1, pos, transform.rotation) as GameObject).transform.parent = transform;
		Instantiate (Cube1, pos, transform.rotation);

		if (active && craneType == CraneType.speedDrop) {
			dropReady = false;
			transform.GetComponent<Renderer>().material.color = Color.red;
			StartCoroutine(SetDropReady(dropDelay));
			
		}

	}
	IEnumerator SpawnCall(float wait) {
		SpawnNewObject ();
		yield return new WaitForSeconds (wait);


		if (active && craneType == CraneType.craneForce) {
			StartCoroutine(SpawnCall(Random.Range (3,5)));
		}

	}

	void OnGUI() {
		//score = score / (ts.GetTilt() * 100);
		GUI.Label (new Rect (15, 15, 150, 25), "Score: " + score.ToString ()+ " / "+completedLevel.ToString());
		GUI.Label (new Rect (15, 45, 150, 25), "Level: " + (int)(Application.loadedLevel+1)); 
		// add gui for win feedback -> you've won the level. Level ending in 3.. 2.. 1.. 
	}
	void WinLevel() {
		Debug.Log ("you've completed the level. gz!");
		StartCoroutine(waitMethod(3.0F));
	}
	void LoseLevel() {
		StartCoroutine (waitMethod (3.0F));
		StartCoroutine (ChangeLevel (Application.loadedLevel));
	}
	IEnumerator waitMethod(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		if (score >= completedLevel) {
			StartCoroutine (ChangeLevel (Application.loadedLevel + 1));
		}
	}
	IEnumerator ChangeLevel(int level) {
		float fadeTime = GameObject.Find ("SceneFader").GetComponent<Fading>().BegindFade(1);
		yield return new WaitForSeconds (fadeTime);
		Application.LoadLevel (level);


	}
	IEnumerator SetDropReady (float dropDelay) {
		yield return new WaitForSeconds (dropDelay);
		dropReady = true;
		transform.GetComponent<Renderer>().material.color = Color.green;
	}
}

