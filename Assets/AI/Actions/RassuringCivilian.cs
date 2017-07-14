using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class RassuringCivilian : RAINAction
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
        parentGO.GetComponent<Adam>().SayRassuring();
        return ActionResult.SUCCESS;
    }
}