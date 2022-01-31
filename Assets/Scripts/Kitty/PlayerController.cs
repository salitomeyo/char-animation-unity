using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float mouseLeftButton = Input.GetAxis("Fire1");
        float mouseRightButton = Input.GetAxis("Fire2");

        if (mouseLeftButton != 0)
        {
            GetComponent<Animator>().SetTrigger("Stun");
        }
        GetComponent<Animator>().SetFloat("Suction", mouseRightButton);
    }
}
