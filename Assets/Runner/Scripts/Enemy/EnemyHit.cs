using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHit : MonoBehaviour, IDamagable
{
    public int health;
    public int damage;

    public UnityEvent HitEnemy;

    public float SightRange;
    private void Start()
    {
        HitEnemy.AddListener(Damage);  
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
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            HitEnemy.Invoke();
        }
    }

    
}
