using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectibles : MonoBehaviour, IPickAble
{
    [SerializeField] UnityEvent PickObject;
    [SerializeField] Vector3 rotation;
    [SerializeField] float rotationSpeed;
    

    private void Start()
    {
        PickObject.AddListener(Pick);
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * rotationSpeed);
    }
    public void Pick()
    {
        Destroy(gameObject);
        GameManager.Instance.scoreNum++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PickObject.Invoke();
        }
    }
}
