using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Start()
    {
        if (instance == null)
            instance = this;

        new Upgrade()
        {
            Title = "Test",
            Description = "Do thing",
            Callback = (p) =>
            {
                p.
            }
        };
    }

    void Update()
    {
        
    }

    [Header("Prefab repository")]
    public GameObject StandardBullet;
}
