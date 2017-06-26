using RAIN.Action;
using UnityEngine;



[RAINAction]
public class randomWayPoint : RAINAction
{
    public GameObject WayPointStops = null;
    private GameObject parentGO;
    string rand ;

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        parentGO = ai.Body;
        WayPointStops = parentGO.GetComponent<HumanAI>().NavTargetsGO;
        if (WayPointStops != null)
        {
            Transform[] ts = WayPointStops.GetComponentsInChildren<Transform>();
            int randomInt = Random.Range(1, ts.Length);
            if (WayPointStops.name == "ShoppingStops")
            {
                rand = "T" + randomInt;
            }
            else
            {
                rand = "TS" + randomInt;
            }
        ai.WorkingMemory.SetItem<string>("target", rand);
        }
        base.Start(ai);
        return ActionResult.SUCCESS;
    }
}