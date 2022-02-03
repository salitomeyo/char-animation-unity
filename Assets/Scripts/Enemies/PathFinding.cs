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

    public void RunFromPlayer() {
        // float randomAngle = Random.Range(0f, 360f);
        // Vector3 randomDestination = new Vector3((Mathf.Cos(randomAngle)*playerTransform.position.x-Mathf.Sin(randomAngle)*playerTransform.position.z), playerTransform.position.y, (Mathf.Sin(randomAngle)*playerTransform.position.x+Mathf.Cos(randomAngle)*playerTransform.position.z));
        // agent.SetDestination(randomDestination*2);
        _sight.GetDirection();
        agent.SetDestination(_sight.GetDirection()*-1*_sight.GetDistance());
    }

    //Elimina el destino, detiene al enemigo
    public void StopFollow()
    {
        agent.ResetPath();
    }
}
