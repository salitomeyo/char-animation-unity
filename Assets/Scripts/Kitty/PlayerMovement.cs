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
    // Update is called once per frame
    void Update()
    {
        float dist = speed*Time.deltaTime;
        float rotation = rotationSpeed*Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = -Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");
        float jump = Input.GetAxis("Jump");

        Vector3 dir = new Vector3(vertical, 0, horizontal);
        Vector3 angle = new Vector3(0, mouseX, 0);

        transform.Translate(dir.normalized*dist);
        transform.Rotate(angle.normalized*rotationSpeed);
        
        GetComponent<Animator>().SetFloat("Run", horizontal);
        GetComponent<Animator>().SetFloat("Walk", -horizontal);

        if (jump > 0)
        {
            GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
