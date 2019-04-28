using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    void Awake()
    {
        if (instance == null)
            instance = this;

    }

    void Update()
    {
        
    }

    [Header("Prefab repository")]
    public GameObject StandardBullet;
}
