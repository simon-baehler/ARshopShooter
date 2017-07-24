using UnityEngine;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class ReassuringCivilian : RAINAction
{
    /// <summary>
    /// Stop the movement of the AI and call the methode SayRassuring
    /// </summary>
    /// <param name="ai"></param>
    /// <returns></returns>
    public override ActionResult Execute(AI ai)
    {
        GameObject parentGO = ai.Body;
        parentGO.GetComponent<Animator>().SetFloat("Speed", 0);
        parentGO.GetComponent<Adam>().SayReassuring();
        return ActionResult.SUCCESS;
    }
}