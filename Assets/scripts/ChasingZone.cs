using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingZone : MonoBehaviour {

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
	    if (other.gameObject.name != "suitF01") return;
	    print("ON");
	    other.gameObject.SendMessage("OnChasingModeOn");
    }
    private void OnTriggerExit(Collider other)
    {
	    if (other.gameObject.name != "suitF01") return;
	    print("OFF");
	    other.gameObject.SendMessage("OnChasingModeOff");
    }

}
