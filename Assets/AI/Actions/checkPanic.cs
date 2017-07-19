using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;

[RAINAction]
public class checkPanic : RAINAction
{
    private string state = "";
    private RAINAspect civil;
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        civil = ai.WorkingMemory.GetItem<RAINAspect>("moveTarget");
        //get the list of nav target of the IA
        state = civil.MountPoint.gameObject.GetComponent<Civilian>().GetState();
        if (state == EnumState.EStates.Panic.ToString())
        {
            ai.WorkingMemory.SetItem<bool>("targetPanic", true);
            ai.Motor.CloseEnoughDistance = 1;
        }
        else
        {
            ai.WorkingMemory.SetItem<bool>("targetPanic", false);   
        }
        return ActionResult.SUCCESS;
    }
}