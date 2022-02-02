using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    float stopStun = 0;
    float stopSuction = 0;
    private Sight _sight;
    private PathFinding _pathFinding;
    private bool isStuned;

    private void Awake() {
        _sight = GetComponentInParent<Sight>();
        _pathFinding = GetComponentInParent<PathFinding>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        walkControl();
        stunControl();
        suctionControl();
    }

    //Se llama cuando un gameobject con collider tipo trigger entra en contacto con el collider
    private void OnTriggerEnter(Collider other)
    {
        //Detecta si el collider que entro en contacto es nuestro proyectil verificando su tag
        if (other.tag == "Bullet")
        {
            stopStun = Time.time+9f;
            //Activa la animacion de stun
            animator.SetTrigger("Stun");
            isStuned = true;
        }
        if (other.name == "SuctionCollider")
        {
            stopSuction = Time.time+6.5f;
            //activa la animacion de suction
            animator.SetFloat("Suction", 1);
            isStuned = true;
        }
    }

    //Controla el reset de la animacion de stun
    private void stunControl()
    {
        if (Time.time > stopStun && stopStun != 0)
        {
            animator.ResetTrigger("Stun");
            isStuned = false;
            stopStun = 0;
        }
    }

    //Controla el reset de la animacion de suction
    private void suctionControl()
    {
        if (Time.time > stopSuction && stopSuction != 0)
        {
            animator.SetFloat("Suction", 0);
            isStuned = false;
            stopSuction = 0;
        }
    }

    private void walkControl()
    {
        //Si el player o alguno de sus ataques entra en el rango de vision activa el path finding a la posicion del player
        if (_sight.getTarget() != null && !isStuned)
        {
            _pathFinding.FollowPlayer();
            animator.SetFloat("Walk", 1);
        }
        //detiene el path finding al player cuando el skeleton esta siendo atacado
        if (isStuned){
            _pathFinding.StopFollow();
            Debug.Log("pare el burro");
            animator.SetFloat("Walk", 0);
        }
    }
}
