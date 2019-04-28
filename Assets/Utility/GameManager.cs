using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    internal void GameOver()
    {
        LevelGui.Instance.GameOver();
    }

    [Header("Prefab repository")]
    public GameObject StandardBullet;
}
