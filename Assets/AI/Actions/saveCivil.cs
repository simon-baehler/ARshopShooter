using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class saveCivil : RAINAction
{
    GameObject goCivil = null;
    Civillian civillianComponent = null;
    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        if (ai.WorkingMemory.GetItem<GameObject>("varCivil") != null)
        {
            //print("varCivil");
            goCivil = ai.WorkingMemory.GetItem<GameObject>("varCivil");
            ai.WorkingMemory.SetItem<GameObject>("moveTo", goCivil);
            civillianComponent = goCivil.gameObject.GetComponent<Civillian>();
            ai.WorkingMemory.SetItem<bool>("targetIsAlife", civillianComponent.isAlife());
            string targetState = ai.WorkingMemory.GetItem<string>("state");
            if (targetState == "dead" || targetState == "panic")
                {
                ai.WorkingMemory.SetItem<bool>("action", false);
                var choix = Random.Range(0, 1);
                if (choix == 0)
                {
                    ai.WorkingMemory.SetItem<string>("state", "run");
                }
                else
                {
                    ai.WorkingMemory.SetItem<string>("state", "panic");
                }
            }

        }
        return ActionResult.SUCCESS;
    }
}