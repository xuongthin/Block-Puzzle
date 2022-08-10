using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region  Cache
    [SerializeField] private BlocksSetting blocksSetting;
    [SerializeField] private BlockSetting blockSetting;
    private int score;
    #endregion

    #region Public access
    public BlocksSetting BlocksSetting => blocksSetting;
    public BlockSetting BlockSetting => blockSetting;
    public int Score => score;

    public Action OnGameStart;
    public Action OnBlockPlaced;
    public Action OnGameOver;
    #endregion

    private void Start()
    {
        Application.targetFrameRate = 60;
        StartCoroutine(StartGame());

        OnBlockPlaced += delegate ()
        {

        };

        OnGameOver += delegate ()
        {

        };
    }

    public void AddScore(int value, bool isCombo = false)
    {
        int additionalScore;

        if (isCombo)
        {
            switch (value)
            {
                case 1:
                    additionalScore = 10;
                    break;
                case 2:
                    additionalScore = 30;
                    break;
                case 3:
                    additionalScore = 60;
                    break;
                default:
                    additionalScore = value * 40;
                    break;
            }
        }
        else
        {
            additionalScore = value;
        }
        UIManager.Instance.QueueAddScore(additionalScore);
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
