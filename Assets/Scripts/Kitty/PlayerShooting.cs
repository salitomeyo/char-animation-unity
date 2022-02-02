using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Cualquier prefab que desee que sea su proyectil")]
    private GameObject prefab;

    // Update is called once per frame
    public void createBullet()
    {
        Instantiate(prefab, new Vector3(transform.position.x+0.2f, transform.position.y+0.45f, transform.position.z), transform.rotation);
    }
}
