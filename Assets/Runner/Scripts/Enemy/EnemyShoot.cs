using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float SightRange;

    [SerializeField] PlayerMovement player;

    [SerializeField] Transform ShootPoint;

    [SerializeField] GameObject EnemyBullet;

    [SerializeField] float bulletSpeedMultiplayer = 10f;

    Rigidbody rb;
    public void ShootPlayer()
    {
        var bullet = Instantiate(EnemyBullet, ShootPoint.position, ShootPoint.rotation);
        Vector3 shootDir = GameManager.instance.player.transform.position - ShootPoint.position;
        rb.velocity = shootDir * bulletSpeedMultiplayer;
    }

   
}
