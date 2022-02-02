using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Cualquier prefab que desee que sea su proyectil")]
    private GameObject prefab;

    [SerializeField]
    [Range(0,10)]
    [Tooltip("En cuanto tiempo luego de ser instanciado se destruira su proyectil")]
    private float bulletLifeTime;

    private GameObject bullet;

    // Update is called once per frame
    public void createBullet()
    {
        bullet = Instantiate(prefab, new Vector3(transform.position.x+0.3f, transform.position.y+0.45f, transform.position.z), transform.rotation);
        Destroy(bullet, bulletLifeTime);
    }

    public GameObject getBullet()
    {
        return bullet;
    }
}
