using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject Drone;
    public static LevelManager Instance = null;

    public List<Level> levels = new List<Level>();
    public static int CurrentLevel = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        levels.AddRange(
            new List<Level>
            {
                // Level 0
                new Level
                {
                    Groups = new List<SpawnGroup>
                    {
                        new SpawnGroup
                        {
                            Quantity = 5,
                            Enemy = Drone,
                        },
                        new SpawnGroup
                        {
                            Quantity = 10,
                            Enemy = Drone,
                        }
                    },
                },
                // Level 1
                new Level
                {
                    Groups = new List<SpawnGroup>
                    {
                        new SpawnGroup
                        {
                            Quantity = 300,
                            Enemy = Drone
                        }
                    }
                }
                // Level 2
            }
        );

        StartWave();
    }

    private void StartWave()
    {
        Spawn(CurrentLevel);
    }

    public void WaveCompleted()
    {
        LevelGui.Instance.LevelCompleted(CurrentLevel);
        StartCoroutine(_WaveCompleted());
    }

    private IEnumerator _WaveCompleted()
    {
        CurrentLevel++;
        yield return new WaitForSeconds(5);
        StartWave();
    }

    public static void OnEnemyDied()
    {
        if (Instance.transform.childCount == 1) // It counts itself as a child
        {
            Instance.WaveCompleted();
        }
    }

    private void Spawn(int currentLevel)
    {
     
        if (currentLevel + 1 <= levels.Count)
        {
            var level = levels[currentLevel];
            foreach (var group in level.Groups)
            {
                for (int i = 0; i < group.Quantity; i++)
                {
                    var obj = Instantiate(group.Enemy, new Vector2(Random.Range(-10f, 10f), 7.5f + Random.Range(-2, 2f)), Quaternion.identity, transform);
                }
            }
        }
        else
        {
            Debug.Log("No more waves, you win. Restarting levels for now tho");
            CurrentLevel = 0;
            WaveCompleted();
            // win?
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector2(20, 4));
    }
}

public class SpawnGroup
{
    public int Quantity { get; set; }
    public GameObject Enemy { get; set; }
}

public class Level
{
    public List<SpawnGroup> Groups { get; set; }
}