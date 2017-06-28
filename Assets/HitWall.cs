using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{
	private int force = 3;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision c)
	{
		print("hit");
		// Calculate Angle Between the collision point and the player
		Vector3 dir = c.contacts[0].point - transform.position;
		// We then get the opposite (-Vector3) and normalize it
		dir = -dir.normalized;
		// And finally we add force in the direction of dir and multiply it by force. 
		// This will push back the player
		GetComponent<Rigidbody>().AddForce(dir*force);
		//Destroy(other.gameObject);
	}
}
