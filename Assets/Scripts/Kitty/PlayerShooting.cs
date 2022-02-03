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

    [SerializeField]
    [Tooltip("El punto del que saldran los proyectiles")]
    private GameObject shootingPoint;

    // Update is called once per frame
    public void createBullet()
    {
        bullet = Instantiate(prefab, new Vector3(shootingPoint.transform.position.x, shootingPoint.transform.position.y, shootingPoint.transform.position.z), shootingPoint.transform.rotation);
        Destroy(bullet, bulletLifeTime);
    }

    public GameObject getBullet()
    {
        return bullet;
    }
}
