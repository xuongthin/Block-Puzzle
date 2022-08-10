using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Cache
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject gameOverGroup;
    [SerializeField] private Text bestScore;
    [SerializeField] private Text score;
    [SerializeField] private float addScoreDelay;
    private Queue<int> addScoreQueue;

    private int _bestScore;
    private int _score;
    private bool isReadyToAddScore;

    private const string BEST_SCORE = "BestScore";
    #endregion

    #region Public access
    public float UIScale => rectTransform.localScale.x;
    #endregion

    private void Start()
    {
        addScoreQueue = new Queue<int>();
        GameManager.Instance.OnGameStart += OnGameStart;
        GameManager.Instance.OnGameOver += ShowGameOver;
    }

    public void OnGameStart()
    {
        addScoreQueue.Clear();
        Helper.GetPlayerPref(out _bestScore, BEST_SCORE, 0);
        bestScore.text = _bestScore.ToString();
        score.text = "0";
    }

    public void AddScore(int additionalScore)
    {
        if (addScoreQueue.Count > 0)
        {
            addScoreQueue.Enqueue(additionalScore);
            StartCoroutine(UpdateScore());
        }
        else
        {
            addScoreQueue.Enqueue(additionalScore);
        }
    }

    private IEnumerator UpdateScore()
    {
        int additionalScore = addScoreQueue.Dequeue();
        UpdateScore(additionalScore);
        yield return Yielders.Get(addScoreDelay);
        if (addScoreQueue.Count > 0)
        {
            StartCoroutine(UpdateScore());
        }
    }

    private void UpdateScore(int additionalScore)
    {
        _score += additionalScore;
        score.text = _score.ToString();
        if (_score > _bestScore)
        {
            _bestScore = _score;
            bestScore.text = _bestScore.ToString();
            PlayerPrefs.SetInt(BEST_SCORE, _bestScore);
        }
    }

    private void ShowGameOver()
    {
        gameOverGroup.SetActive(true);
    }
}
