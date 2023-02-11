using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectibles : MonoBehaviour, IPickAble
{
    public UnityEvent PickObject;
    public GameManager gameManager;

    private void Start()
    {
        PickObject.AddListener(Pick);
    }
    public void Pick()
    {
        Destroy(gameObject);
        gameManager.scoreNum++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickObject.Invoke();
        }
    }
}
