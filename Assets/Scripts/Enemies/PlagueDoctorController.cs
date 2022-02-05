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
    private PlayerShooting _playerShooting;
    private bool isStuned;
    private bool isRunning;
    private bool isShooting;

    private float shootingDelay;

    private void Awake() {
        _sight = GetComponentInParent<Sight>();
        _pathFinding = GetComponentInParent<PathFinding>();
        _playerShooting = GetComponentInParent<PlayerShooting>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Wander", 0, 5);
        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        walkControl();
        suctionControl();
        attackControl();
        stunControl();

        if (_sight.GetDistance() < 100 && !isStuned)
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
            stopStun = Time.time+13f;
            //Activa la animacion de stun
            animator.SetTrigger("Stun");
            isStuned = true;
        }
        if (other.name == "SuctionCollider")
        {
            stopSuction = Time.time+2f;
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
        if (stopSuction != 0 && stopSuction-Time.time  < 1.5f && stopSuction-Time.time  > 1.48f)
        {
            _pathFinding.RunFromPlayer();
            stopSuction = 0;
        }

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
        else if (isStuned)
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
        }

        else if (!isStuned && (_sight.GetDistance() < 4f && _sight.GetDistance() != 100f))
        {
            _pathFinding.StopFollow();
            animator.SetFloat("Walk", 0);
            animator.SetFloat("Attack", 1);

            if ( (animator.GetCurrentAnimatorStateInfo(0).IsName("attack_1") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))){ 
                shootingDelay = Time.time+1.6f;
                isShooting = true;
            }
            if (isShooting)
            {
                if (Time.time > shootingDelay)
                {
                    _playerShooting.createBullet();
                    isShooting = false;
                    shootingDelay = 0;
                    animator.SetFloat("Attack", 0);
                }
            }
            // Invoke("Shoot", 5);
        }
    }

    private void Wander()
    {
        if (!isStuned && _sight.getTarget() == null)
        {
            _pathFinding.WanderAbout(0.5f);
            animator.SetFloat("Walk", 1);
        }
    }

    private void Shoot()
    {
        _playerShooting.createBullet();
    }
}
