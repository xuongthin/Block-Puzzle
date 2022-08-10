using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameGroup : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Text score;
    [SerializeField] private Text best;


    public void Show(int score, int best)
    {
        this.score.text = score.ToString();
        this.best.text = best.ToString();
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        StartCoroutine(Show(1f, 1.5f));
    }

    // Temp
    IEnumerator Show(float delay, float time)
    {
        yield return Yielders.Get(delay);
        canvasGroup.interactable = false;
        float lerp = 0;
        while (lerp < time)
        {
            lerp += Time.deltaTime;
            canvasGroup.alpha = lerp / time;
            yield return null;
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
    }
}
