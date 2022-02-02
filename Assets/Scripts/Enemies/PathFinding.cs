using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerTransform;
    private Sight _sight;

    private void Awake() {
        _sight = GetComponent<Sight>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        if (_sight.getTarget() != null)
        {
            FollowPlayer();
        }
    }
    
    void FollowPlayer() {
        agent.SetDestination(playerTransform.position);
    }
}
