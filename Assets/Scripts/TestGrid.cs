using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestGrid : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2Int id = Grid.Instance.Position2Cell(eventData.position);
        Debug.Log(id);
    }
}
