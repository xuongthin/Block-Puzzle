using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestGrid : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        int id = Grid.Instance.GetCellIdAt(eventData.position);
        Debug.Log(id);
    }
}
