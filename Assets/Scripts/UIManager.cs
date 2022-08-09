using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private RectTransform rectTransform;
    [SerializeField] private GameObject gameOverGroup;
    public float UIScale => rectTransform.localScale.x;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GameManager.Instance.OnGameOver += ShowGameOver;
    }

    private void ShowGameOver()
    {
        gameOverGroup.SetActive(true);
    }
}
