using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator animator;
    float startStun = 0;
    float startSuction = 0;
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
        suctionControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("oeeeeeeeeeee");
        if (other.name == "LightStun")
        {
            startStun = Time.time;
            animator.SetTrigger("Stun");
            isStuned = true;
        }
        if (other.name == "SuctionCollider")
        {
            animator.SetFloat("Suction", 1);
            startSuction = Time.time;
        }
    }

    private void stunControl()
    {
        if (Time.time > startStun+9)
        {
            animator.ResetTrigger("Stun");
            isStuned = false;
            startStun = 0;
        }
    }

    private void suctionControl()
    {
        if (Time.time > startSuction+6.5)
        {
            animator.SetFloat("Suction", 0);
            startSuction = 0;
        }
    }

    public bool getStun()
    {
        return isStuned;
    }
}
