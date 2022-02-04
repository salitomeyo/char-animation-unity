using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab del enemigo que se spawneara en oleadas")]
    private GameObject prefab;

    [SerializeField]
    [Tooltip("Tiempo en que inicia la oleada")]
    private float startTime;

    [SerializeField]
    [Tooltip("Tiempo en que finaliza la oleada")]
    private float endTime;

    [SerializeField]
    [Tooltip("Tiempo entre la generacion de enemigos")]
    private float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startTime, spawnRate);
        Invoke("CancelInvoke", endTime);
    }

    void SpawnEnemy()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
