using UnityEngine;
using System.Collections;
using TMPro;
using System;

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
        // Play sound
        text.text = "WAVE " + level + " ACCOMPLISHED";
        StartCoroutine(ShowTextForAWhile());
    }

    public void GameOver()
    {
        text.text = "YOU LET EVERYONE DIE \r\n You lose.";
        StartCoroutine(ShowTextForAWhile());
    }

    private IEnumerator ShowTextForAWhile()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        text.gameObject.SetActive(false);
    }
}
