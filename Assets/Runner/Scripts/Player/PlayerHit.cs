using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHit : MonoBehaviour, IDamagable
{
    public int health;
    public int damage;

    public UnityEvent HitPlayer;

    private void Start()
    {
        HitPlayer.AddListener(Damage);
    }
    public void Damage()
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HitPlayer.Invoke();
        }
    }
}
