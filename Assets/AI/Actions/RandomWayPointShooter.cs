using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Navigation;
using UnityEngine;
using System.Collections;

[RAINAction]
public class randomWayPointShooter : RAINAction
{
    public GameObject WayPointStops = null;

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        WayPointStops = GameObject.Find("ShooterPoint");
        if (WayPointStops != null)
        {
            Transform[] ts = WayPointStops.GetComponentsInChildren<Transform>();
            int randomInt = Random.Range(1, ts.Length);
            string rand = "shooterpoint" + randomInt;
            ai.WorkingMemory.SetItem<string>("shooterpoint", rand);
        }
        base.Start(ai);
        return ActionResult.SUCCESS;
    }
}