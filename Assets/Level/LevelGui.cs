using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelGui : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static LevelGui Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LevelCompleted(int level)
    {
        text.text = "WAVE " + level + "/10 ACCOMPLISHED";
        StartCoroutine(ShowTextForAWhile());
    }

    public void GameOver()
    {
        text.text = "YOU LET EVERYONE DIE \r\n You lose. \r\n Quitting game...";
        StartCoroutine(RestartGame());
        StartCoroutine(ShowTextForAWhile());
    }

    public void Win()
    {
        text.text = "YOU DID IT! YOU SAVED EVERYONE FROM EXTINCTION! \r\n YOU'RE A TRUE HERO. Quitting game...";
        StartCoroutine(RestartGame());
        StartCoroutine(ShowTextForAWhile());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }

    private IEnumerator ShowTextForAWhile()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        text.gameObject.SetActive(false);
    }
}
