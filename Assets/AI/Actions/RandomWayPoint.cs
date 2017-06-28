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
        //get the list of nav target of the IA
        WayPointStops = parentGO.GetComponent<HumanAI>().NavTargetsGO;
        Transform[] ts = WayPointStops.GetComponentsInChildren<Transform>();
        int randomInt = Random.Range(1, ts.Length-1);
        rand = WayPointStops.transform.GetChild(randomInt).name;
        ai.WorkingMemory.SetItem<string>("target", rand);
        base.Start(ai);
        return ActionResult.SUCCESS;
    }
}