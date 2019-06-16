using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuiMessageManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static GuiMessageManager Instance = null;

    bool isParsingMessages = false;
    List<GuiMessage> guiMessages = new List<GuiMessage>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void DisplayMessage(string message, float duration = 4, Action callback = null)
    {
        guiMessages.Add(
            new GuiMessage {
                Message = message,
                Duration = duration,
                Callback = callback,
            }
        );
        
        if (!isParsingMessages)
            StartCoroutine(ParseMessageQueue());
    }

    private IEnumerator ParseMessageQueue()
    {
        isParsingMessages = true;
        while (guiMessages.Count > 0)
        {
            yield return StartCoroutine(ShowTextForAWhile(guiMessages[0]));
            guiMessages.RemoveAt(0);
            yield return new WaitForSeconds(0.5f);
        }
        isParsingMessages = false;
    }

    private IEnumerator ShowTextForAWhile(GuiMessage msg)
    {
        text.text = msg.Message;
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(msg.Duration);
        text.gameObject.SetActive(false);

        msg.Callback?.Invoke();
    }
}

public class GuiMessage
{
    public string Message { get; set; }
    public float Duration { get; set; }
    public Action Callback { get; set; }
}