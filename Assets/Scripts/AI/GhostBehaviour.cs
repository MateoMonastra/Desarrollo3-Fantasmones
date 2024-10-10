using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField] private Bonsai.Core.BehaviourTree tree;
    [SerializeField] private bool canSeePlayer;

    private void Update()
    {
        tree.blackboard.Set(nameof(canSeePlayer), canSeePlayer);
    }
}
