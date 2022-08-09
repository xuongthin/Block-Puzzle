using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private BlocksSetting blocksSetting;
    [SerializeField] private BlockSetting blockSetting;

    public BlocksSetting BlocksSetting => blocksSetting;
    public BlockSetting BlockSetting => blockSetting;

    public Action OnGameStart;
    public Action OnBlockPlaced;
    public Action OnGameOver;

    private void Awake()
    {
        Instance = this;

        OnBlockPlaced += delegate ()
        {

        };

        OnGameOver += delegate ()
        {

        };
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(StartGame());
    }

    public void Replay()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator StartGame()
    {
        yield return Yielders.Get(0.25f);
        OnGameStart();
    }
}
