using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    float startStun = 0;
    private Sight _sight;
    private bool isStuned;

    private void Awake() {
        _sight = GetComponentInParent<Sight>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        stunControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LightStun")
        {
            animator.SetFloat("Walk", 0);
            startStun = Time.time;
            animator.SetTrigger("Stun");
            isStuned = true;
        }
        if (other.name == "SuctionCollider")
        {
            animator.SetFloat("Suction", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "SuctionCollider")
        {
            animator.SetFloat("Suction", 0);
        }
    }

    private void stunControl()
    {
        if (startStun == 0)
        {
            if (_sight.getTarget() != null)
            {
                animator.SetFloat("Walk", 1);
            }
            if (_sight.getTarget() == null && animator.GetFloat("Walk") == 1)
            {
                animator.SetFloat("Walk", 0);
            }
        }
        if (Time.time > startStun+9.5)
        {
            animator.ResetTrigger("Stun");
            isStuned = false;
            startStun = 0;
        }
    }

    public bool getStun()
    {
        return isStuned;
    }
}
