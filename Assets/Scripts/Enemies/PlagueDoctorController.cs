using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueDoctorController : MonoBehaviour
{
    Animator animator;
    float stopStun = 0;
    float stopSuction = 0;
    private Sight _sight;
    private PathFinding _pathFinding;
    private bool isStuned;
    private bool isRunning;

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
        suctionControl();
        attackControl();
        stunControl();
        _sight.FaceTarget();
    }

    //Se llama cuando un gameobject con collider tipo trigger entra en contacto con el collider
    private void OnTriggerEnter(Collider other)
    {
        //Detecta si el collider que entro en contacto es nuestro proyectil verificando su tag
        if (other.tag == "Bullet")
        {
            stopStun = Time.time+13f;
            //Activa la animacion de stun
            animator.SetTrigger("Stun");
            isStuned = true;
        }
        if (other.name == "SuctionCollider")
        {
            stopSuction = Time.time+2f;
            //activa la animacion de suction
            //animator.SetFloat("Suction", 1);
            _pathFinding.RunFromPlayer();
            _sight.FaceTarget();
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
        if (Time.time > stopSuction && stopSuction != 0 || (animator.GetCurrentAnimatorStateInfo(0).IsName("plaguedoctor_suction_anim") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)))
        {
            animator.SetFloat("Suction", 0);
            isStuned = false;
            stopSuction = 0;
        }
    }

    private void attackControl()
    {
        if ((_sight.GetDistance() > 4f && _sight.GetDistance() != 100f) || isStuned || (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)))
        {
            animator.SetFloat("Attack", 0);
        }
    }

    private void walkControl()
    {
        //Si el player o alguno de sus ataques entra en el rango de vision activa el path finding a la posicion del player
        if (_sight.getTarget() != null && !isStuned && _sight.GetDistance() > 4f && _sight.GetDistance() != 100f)
        {
            _pathFinding.FollowPlayer();
            animator.SetFloat("Walk", 1);
        }

        //detiene el path finding al player cuando el skeleton esta siendo atacado
        if (isStuned || _sight.getTarget() == null || (_sight.GetDistance() < 4f && _sight.GetDistance() != 100f))
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
        }

        if (_sight.GetDistance() < 4f && !isStuned)
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
            animator.SetFloat("Attack", 1);
        }

        // if (_sight.GetDistance() >= 4.5f && !isStuned)
        // {
        //     animator.SetFloat("Walk", 0);
        //     _pathFinding.StopFollow();
        // }

        // if (_sight.GetDistance() >= 4.5f && !isStuned && _sight.getTarget() != null)
        // {
        //     animator.SetFloat("Attack", 1);
        // }

        // if (_sight.GetDistance() < 2.5f && !isStuned && _sight.getTarget() != null)
        // {
        //     Debug.Log("Huyamosss");
        //     _pathFinding.RunFromPlayer();
        //     animator.SetFloat("Walk", 1);
        // }
    }
}
