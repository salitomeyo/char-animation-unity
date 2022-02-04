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
        //Definimos un angulo random, esto permitira que el teletransporte no vaya siempre en la misma direccion
        float randomAngle = Random.Range(0f, 360f);
        //obtenemos el vector direccion al collider
        Vector3 direct = _sight.GetDirectionFromPlayer();
        //Le realizamos al vector una transformacion de rotacion
        Vector3 randomDestination = new Vector3((Mathf.Cos(randomAngle)*direct.x-Mathf.Sin(randomAngle)*direct.z), direct.y, (Mathf.Sin(randomAngle)*direct.x+Mathf.Cos(randomAngle)*direct.z));
        //Translada (teletransporta) al gameObject en la direccion de randomDestination
        agent.transform.Translate((randomDestination*2.5f));
    }

    public void WanderAbout() {
        Vector3 direct = new Vector3(transform.position.x+Random.Range(-3f, 3f), transform.position.y, transform.position.z+Random.Range(-3f, 3f));
        agent.SetDestination(direct);
    }

    //Elimina el destino, detiene al enemigo
    public void StopFollow()
    {
        agent.ResetPath();
    }
}
