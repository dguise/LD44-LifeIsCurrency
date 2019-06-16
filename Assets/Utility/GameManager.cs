using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public static Player Player = null;
    public static GameObject Target = null;

    void Awake()
    {
        Player = GameObject.FindObjectOfType<Player>();
        Target = GameObject.FindObjectOfType<TargetUnit>().gameObject;
        if (Instance == null)
            Instance = this;

    }

    bool _isQuitting = false;
    internal void GameOver()
    {
        if (_isQuitting) return;
        _isQuitting = true;

        GuiMessageManager.Instance.DisplayMessage("The mothership has fallen, you have failed us.", callback: () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }
    

    [Header("Prefab repository")]
    public GameObject StandardBullet;
    public GameObject EnemyStandardBullet;
    public GameObject EnemySiegeBullet;
}
