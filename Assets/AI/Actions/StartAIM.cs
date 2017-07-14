using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Polarith.AI.Move;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class StartAIM : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        if(ai.Body.GetComponent<AIMSimpleController>() != null)
            ai.Body.GetComponent<AIMSimpleController>().Speed = 2;
        base.Start(ai);
    }
}