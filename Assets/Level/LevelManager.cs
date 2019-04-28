using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject Drone;
    public GameObject TougherDrone;
    public GameObject WeakDrone;
    public GameObject Siege;
    public static LevelManager Instance = null;

    public List<Level> levels = new List<Level>();
    public static int CurrentLevel = 0;

    public int debugLevelSkip = 0;
    private void Awake()
    {
        CurrentLevel = debugLevelSkip;
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        levels.AddRange(
            new List<Level>
            {
                // Level 0
                new Level{Groups = new List<SpawnGroup>
                {
                   new SpawnGroup{Quantity = 1,Enemy = Drone},
                }},
                // Level 1
                new Level{Groups = new List<SpawnGroup>
                {
                   new SpawnGroup{Quantity = 3,Enemy = Drone},
                   new SpawnGroup{Quantity = 1,Enemy = WeakDrone},
                }},
                // Level 2
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 10,Enemy = Drone},
                }},
                // Level 3
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 1,Enemy = Drone},
                    new SpawnGroup{Quantity = 20,Enemy = WeakDrone},
                }},
                // Level 4
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 5,Enemy = Drone},
                    new SpawnGroup{Quantity = 1,Enemy = Siege},
                }},
                // Level 5
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 2,Enemy = TougherDrone},
                }},
                // Level 6
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 50,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 1,Enemy = TougherDrone},
                }},
                // Level 7
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 1,Enemy = Siege},
                }},
                // Level 8
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 50,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 50,Enemy = Drone},
                    new SpawnGroup{Quantity = 5,Enemy = TougherDrone},
                }},
                // Level 9
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 100,Enemy = Drone},
                    new SpawnGroup{Quantity = 5,Enemy = TougherDrone},
                    new SpawnGroup{Quantity = 5,Enemy = Siege},
                }},
                // Level 10
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 100,Enemy = Drone},
                    new SpawnGroup{Quantity = 50,Enemy = TougherDrone},
                    new SpawnGroup{Quantity = 30,Enemy = Siege},
                }},


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
        GameManager.Player.Health += GameManager.Player.RepairPerRound; // TODO: Play some sound/particle with this?
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