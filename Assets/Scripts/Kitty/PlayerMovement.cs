using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    [Tooltip("Velocidad de movimiento del personaje en m/s")]
    private float speed;

    [SerializeField]
    [Range(0, 360)]
    [Tooltip("Velocidad de la rotacion del personaje en grados/s")]
    private float rotationSpeed; 

    AudioSource audioData;
    Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        audioData = GetComponent<AudioSource>();
    }
    void Update()
    {
        //cambio en la distancia en un seg
        float dist = speed*Time.deltaTime;
        //cambio en la rotacion en un segundo
        float rotation = rotationSpeed*Time.deltaTime;

        //Input.GetAxis accede a los Inputs definidos en projects settings > input manager
        //Input.GetAxis devuelve valores entre -1 y 1
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float jump = Input.GetAxis("Jump");

        //Vector direccion del movimiento
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        //Vector angulo de rotacion
        Vector3 angle = new Vector3(0, mouseX, 0);

        transform.Translate(dir.normalized*dist);
        transform.Rotate(angle.normalized*rotationSpeed);
        
        animator.SetFloat("Walk", (Mathf.Abs(horizontal)+Mathf.Abs(vertical))/2);

        // if (jump > 0)
        // {
        //     animator.SetTrigger("Jump");
        // }
    }
}
