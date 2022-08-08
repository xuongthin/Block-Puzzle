using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private BlocksSetting blocksSetting;
    [SerializeField] private BlockSetting blockSetting;
    private Camera mainCamera;

    public BlocksSetting BlocksSetting => blocksSetting;
    public BlockSetting BlockSetting => blockSetting;

    public Action OnGameStart;
    public Action OnGameOver;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        mainCamera = Camera.main;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return Yielders.Get(0.25f);
        OnGameStart();
    }
}
