using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoosePosition : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(gameObject.transform.position + gameObject.name);
        ControllerTTT.instance.PlaceShape(gameObject);
    }
}
