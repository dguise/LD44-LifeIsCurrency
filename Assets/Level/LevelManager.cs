using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public GameObject Drone;
    public GameObject TougherDrone;
    public GameObject WeakDrone;
    public GameObject Siege;
    public GameObject Carrier;
    public static LevelManager Instance = null;

    public delegate void WaveComplete();
    public event WaveComplete OnWaveComplete;

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
                // Level 1
                new Level{Groups = new List<SpawnGroup>
                {
                   new SpawnGroup{Quantity = 1,Enemy = Drone},
                }},
                // Level 2
                new Level{Groups = new List<SpawnGroup>
                {
                   new SpawnGroup{Quantity = 3,Enemy = Drone},
                   new SpawnGroup{Quantity = 1,Enemy = WeakDrone},
                }},
                // Level 3
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 10,Enemy = Drone},
                }},
                // Level 4
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 1,Enemy = Drone},
                    new SpawnGroup{Quantity = 20,Enemy = WeakDrone},
                }},
                // Level 5
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 1,Enemy = Carrier},
                }},
                // Level 6
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 5,Enemy = Drone},
                    new SpawnGroup{Quantity = 1,Enemy = Siege},
                }},
                // Level 7
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 2,Enemy = TougherDrone},
                }},
                // Level 8
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 50,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 1,Enemy = TougherDrone},
                }},
                // Level 9
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 1,Enemy = Siege},
                }},
                // Level 10
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 50,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 50,Enemy = Drone},
                    new SpawnGroup{Quantity = 5,Enemy = TougherDrone},
                }},
                // Level 11
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 100,Enemy = Drone},
                    new SpawnGroup{Quantity = 5,Enemy = TougherDrone},
                    new SpawnGroup{Quantity = 5,Enemy = Siege},
                }},
                // Level 12
                new Level{Groups = new List<SpawnGroup>
                {
                    new SpawnGroup{Quantity = 100,Enemy = WeakDrone},
                    new SpawnGroup{Quantity = 100,Enemy = Drone},
                    new SpawnGroup{Quantity = 50,Enemy = TougherDrone},
                    new SpawnGroup{Quantity = 30,Enemy = Siege},
                    new SpawnGroup{Quantity = 2,Enemy = Carrier},
                }},


            }
        );

        RoundDone();
    }

    // Round = kill enemis + do upgrades
    public void RoundDone()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(2);
        GuiMessageManager.Instance.DisplayMessage($"ALERT!\r\nEnemies incoming", 1);
        yield return new WaitForSeconds(2);
        Spawn(CurrentLevel);
    }


    public void WaveCompleted()
    {
        GuiMessageManager.Instance.DisplayMessage($"Wave {CurrentLevel+1}/{LevelManager.Instance.levels.Count} accomplished");
        CurrentLevel++;
        GameManager.Player.ReduceHp(-GameManager.Player.RepairPerRound); // TODO: Play some sound/particle with this?

        OnWaveComplete?.Invoke();
    }

    Coroutine enemyDiedDebouncer;
    public void OnEnemyDied()
    {
        if (enemyDiedDebouncer != null)
            StopCoroutine(enemyDiedDebouncer);
        enemyDiedDebouncer = StartCoroutine(CheckAllEnemiesDead());
    }

    IEnumerator CheckAllEnemiesDead()
    {
        yield return new WaitForSeconds(1);
        if (Instance.transform.childCount == 0)
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
            GuiMessageManager.Instance.DisplayMessage("YOU DID IT! YOU SAVED EVERYONE FROM EXTINCTION! \r\n YOU'RE A TRUE HERO!");
        }
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