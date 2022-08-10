using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageGroup : MonoBehaviour
{
    [SerializeField] private Text message;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        this.message.text = message;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
