using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerReceiver : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent OnPointerDownCallBack;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke();
    }
}
