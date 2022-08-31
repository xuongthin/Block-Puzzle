using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualGridTester : MonoBehaviour
{
    public VirtualGrid grid;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int cellId = grid.Position2CellId(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(cellId);
        }
    }
}
