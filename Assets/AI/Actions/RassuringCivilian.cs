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
    string rand ;
      

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        parentGO = ai.Body;
        parentGO.GetComponent<Animator>().SetFloat("Speed", 0);
        parentGO.GetComponent<Adam>().sayRassuring();
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}