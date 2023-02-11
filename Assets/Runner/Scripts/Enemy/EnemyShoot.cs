using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float SightRange;

    public PlayerMovement player;

    public Transform ShootPoint;

    public GameObject EnemyBullet;

    public float bulletSpeedMultiplayer = 10f;

    Rigidbody rb;
    public void ShootPlayer()
    {
        var bullet = Instantiate(EnemyBullet, ShootPoint.position, ShootPoint.rotation);
        Vector3 shootDir = GameManager.instance.player.transform.position - ShootPoint.position;
        rb.velocity = shootDir * bulletSpeedMultiplayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, (GameManager.instance.player.transform.position - transform.position).normalized * SightRange);
    }
}
