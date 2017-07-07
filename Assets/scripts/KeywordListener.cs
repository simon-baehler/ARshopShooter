﻿using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class KeywordListener : MonoBehaviour
{
	private GazeManager gm;
	// Use this for initialization
	void Start () {
		gm = gameObject.GetComponentInChildren<GazeManager>();
	}
	public void OnSayPolice()
	{
		if (gm.HitObject != null && gm.HitObject.GetComponent<Civilian>() != null)
		{
			//gm.HitObject.GetComponent<Civilian>().OnPolice();
			gm.HitObject.GetComponent<Civilian>().SendMessage("OnPolice");
		}
	}
}
