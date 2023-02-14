using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] float SightRange;

    [SerializeField] Transform ShootPoint;

    [SerializeField] GameObject EnemyBullet;

    [SerializeField] float bulletSpeedMultiplayer;

    [SerializeField] Rigidbody rb;

    [SerializeField] bool IsPlayerSeen;
    private void Update()
    {
        if (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) < SightRange)
        {
            ShootPlayer();
        }
    }
    public void ShootPlayer()
    {
        EnemyBullet.SetActive(true);
        Vector3 shootDir = GameManager.instance.player.transform.position - ShootPoint.position;
        rb.velocity = shootDir * bulletSpeedMultiplayer;
       
    }



}
