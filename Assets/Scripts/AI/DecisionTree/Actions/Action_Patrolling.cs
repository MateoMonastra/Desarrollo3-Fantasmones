using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Patrolling : AI.DecisionTree.TreeAction
{
    public Action OnPatrolling;

    public override void NodeFunction()
    {
        OnPatrolling?.Invoke();
    }
}
