using UnityEngine;
using System.Collections;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class LevelGui : MonoBehaviour
{
    public TextMeshProUGUI text;
    public static LevelGui Instance = null;

    private bool _isQuitting = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LevelCompleted(int level)
    {
        if (_isQuitting) return;

        level = level + 1;
        text.text = "WAVE " + level + "/" + LevelManager.Instance.levels.Count + " ACCOMPLISHED";
        StartCoroutine(ShowTextForAWhile());
    }

    
    public void GameOver()
    {
        if (_isQuitting) return;

        text.text = "YOU LET EVERYONE DIE \r\n You lose. \r\n ...";
        StartCoroutine(ShowTextForAWhile(RestartGame));
    }

    public void Win()
    {
        if (_isQuitting) return;

        text.text = "YOU DID IT! YOU SAVED EVERYONE FROM EXTINCTION! \r\n YOU'RE A TRUE HERO. Quitting game...";
        StartCoroutine(ShowTextForAWhile());
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator ShowTextForAWhile(Action callback = null)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        text.gameObject.SetActive(false);

        callback?.Invoke();
    }
}
