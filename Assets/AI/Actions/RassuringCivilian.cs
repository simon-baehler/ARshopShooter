using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RassuringCivilian : RAINAction
{
    public GameObject WayPointStops = null;
    private GameObject parentGO;
      
    public override ActionResult Execute(AI ai)
    {
        parentGO = ai.Body;
        parentGO.GetComponent<Animator>().SetFloat("Speed", 0);
        parentGO.GetComponent<Adam>().SayRassuring();
        return ActionResult.SUCCESS;
    }
}