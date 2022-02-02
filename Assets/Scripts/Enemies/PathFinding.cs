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
    
    //Usa setDestination asignando la posicion del player para seguirlo
    public void FollowPlayer() {
        agent.SetDestination(playerTransform.position);
    }

    //Elimina el destino, detiene al enemigo
    public void StopFollow()
    {
        agent.ResetPath();
    }
}
