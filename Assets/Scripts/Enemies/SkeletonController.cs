using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    float startStun;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Time.time > startStun+2)
        {
            animator.ResetTrigger("Stun");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "LightStun")
        {
            startStun = Time.time;
            animator.SetTrigger("Stun");
        }
        if (other.name == "SuctionCollider")
        {
            animator.SetFloat("Suction", 1);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.name == "SuctionCollider")
        {
            animator.SetFloat("Suction", 0);
        }
    }
}
