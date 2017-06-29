using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{
	private int force = 3;
	private Vector3 oldLocation = new Vector3();
	// Use this for initialization
	void Start () {
		oldLocation = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		oldLocation = transform.position;
	}

	private void OnCollisionEnter(Collision c)
	{
		print("hit");
		oldLocation = transform.position;
	}
}
