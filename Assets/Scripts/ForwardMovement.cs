using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    [Tooltip("Velocidad de la bala en m/s")]
    private float speed;

    // Update is called once per frame
    void Update()
    {
        float dist = speed*Time.deltaTime;

        transform.Translate(0, 0, dist);
    }
}
