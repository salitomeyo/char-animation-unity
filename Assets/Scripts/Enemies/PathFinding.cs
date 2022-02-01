using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    private void Awake() {
        _sight = GetComponent<Sight>();
        agent = GetComponentInParent<NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (_sight.getTarget() != null)
        {
            FollowPlayer();
        }
    }
    
    void FollowPlayer() {
        agent.SetDestination(playerTransform);
    }
}
