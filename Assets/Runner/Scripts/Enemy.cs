using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamaged
{
    public int health;
    public int damage;

  
    public void Damage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Destroy(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Damage(damage);
        }
    }
}
