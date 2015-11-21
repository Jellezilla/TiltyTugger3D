using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	
	Rigidbody rigid;
	public float speed = 10.0F;
	public float maxSpeed = 10.0F;
	public float jumpSpeed = 10.0F;
	
	private float currentLane;
	// Use this for initialization
	void Start () {
		rigid = transform.GetComponent<Rigidbody> ();
		currentLane = 0.0F; // Mid lane
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// --- Inputs 
		if(Input.GetKey (KeyCode.LeftArrow)) {
			rigid.AddForce(-Vector3.right * speed, ForceMode.Force);
			
		}
		if(Input.GetKey (KeyCode.RightArrow)) {
			rigid.AddForce(Vector3.right * speed, ForceMode.Force);
		}
		
		/*		if(Input.GetKeyDown (KeyCode.UpArrow)) {
			currentLane += .75F;
			//if(currentLane>1.75F) 
			//{
			//	currentLane = 1.0F;
			//}
		}
		if(Input.GetKeyDown (KeyCode.DownArrow)) {
			currentLane -= .75F;
			//if(currentLane<.125F) {
			//	currentLane = .125F;
			//}
		}
*/
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log("jump!");
			rigid.AddForce(Vector3.up * jumpSpeed/2, ForceMode.Impulse);
		}
	}
	void FixedUpdate() {
		
		
		
		
		// --- Restrictions
		
		// Force the player to move in a static z position
		transform.position = new Vector3 (transform.position.x, transform.position.y, currentLane);
		
		// If player velocity exceeds maxSpeed, set it to maxSpeed. 
		if(rigid.velocity.magnitude > maxSpeed)
		{
			rigid.velocity = rigid.velocity.normalized * maxSpeed;
		}
		
		
		
	}
}
