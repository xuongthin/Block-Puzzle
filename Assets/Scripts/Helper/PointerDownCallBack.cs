using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PointerDownCallBack : MonoBehaviour, IPointerDownHandler
{
    public UnityEventCallBackVector2 OnPointerDownCallBack;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownCallBack?.Invoke(eventData.position);
    }
}
