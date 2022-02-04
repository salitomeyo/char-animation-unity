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

    private Kill _kill;

    private void Awake() {
        _sight = GetComponentInParent<Sight>();
        _pathFinding = GetComponentInParent<PathFinding>();
        _kill = GetComponentInParent<Kill>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Wander", 1, 4);
        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        walkControl();
        attackControl();
        stunControl();
        suctionControl();
        if (animator.GetFloat("Suction") == 1)
        {
            _sight.FaceTarget();
        }
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
            _sight.FaceTarget();
            isStuned = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "SuctionCollider")
        {
            stopSuction = 0;
            animator.SetFloat("Suction", 0);
            isStuned = false;
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_dead_anim") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))
        {
            animator.SetFloat("Suction", 0);
            isStuned = false;
            stopSuction = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_dead_anim") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.98 && _sight.getTarget() == null)
        {
            animator.SetFloat("Suction", 0);
            isStuned = false;
            stopSuction = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("skeleton_dead_anim") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98)
        {
            _kill.SelfDestroy();
        }
    }

    private void attackControl()
    {
        if ((_sight.GetDistance() > 1f && _sight.GetDistance() != 100f) || isStuned || (animator.GetCurrentAnimatorStateInfo(0).IsName("Strike_2") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)))
        {
            animator.SetFloat("Attack", 0);
        }
    }

    private void walkControl()
    {
        //Si el player o alguno de sus ataques entra en el rango de vision activa el path finding a la posicion del player
        if (_sight.getTarget() != null && !isStuned && _sight.GetDistance() > 1f && _sight.GetDistance() != 100f)
        {
            _pathFinding.FollowPlayer();
            animator.SetFloat("Walk", 1);
        }

        //detiene el path finding al player cuando el skeleton esta siendo atacado
        else if (isStuned)
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
        }

        else if (!isStuned && (_sight.GetDistance() < 1f && _sight.GetDistance() != 100f))
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
            animator.SetFloat("Attack", 1);
        }
    }

    private void Wander()
    {
        if (!isStuned && _sight.getTarget() == null)
        {
            _pathFinding.WanderAbout();
            animator.SetFloat("Walk", 1);
        }
    }
}
