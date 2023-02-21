using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHit : MonoBehaviour, IDamagable
{
    [SerializeField] int health;
    [SerializeField] int damage;
    [SerializeField] UnityEvent HitPlayer;

    private void Start()
    {
        HitPlayer.AddListener(Damage);
    }
    public void Damage()
    {
        health -= damage;
        if (health <= 0)
        {
            GameManager.Instance.OnPlayerDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            HitPlayer.Invoke();
            Destroy(collision.gameObject);
        }
        
    }
}
