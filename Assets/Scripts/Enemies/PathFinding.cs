using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private Transform playerTransform;
    private Sight _sight;
    private SkeletonController _skeletonController;

    private void Awake() {
        _sight = GetComponent<Sight>();
        _skeletonController = GetComponentInChildren<SkeletonController>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update() {
        bool stun = _skeletonController.getStun();
        Debug.Log(stun);
        if (_sight.getTarget() != null && !stun)
        {
            FollowPlayer();
        }
    }
    
    void FollowPlayer() {
        agent.SetDestination(playerTransform.position);
    }
}
