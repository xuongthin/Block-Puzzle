using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private RectTransform rectTransform;
    public float UIScale => rectTransform.localScale.x;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
