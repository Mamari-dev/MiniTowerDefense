using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private EnemyType[] enemyTypes;
    [SerializeField] private int startAmount = 4;
    [SerializeField] private float amountScaling = 1.15f;
    [SerializeField] private float spawnDelay = 1f;
    private int enemyTypeNumber = 0;
    private int waveCount = 1;
    private int currentEnemyAmount;

    private List<WaveManagerSpawnDatas> spawningEnemys = new();

    public static Action StartWave;
    public static Action EndWave;

    private void Start()
    {
        AddSpawningEnemy();
    }

    private void AddSpawningEnemy()
    {
        if (enemyTypeNumber >= enemyTypes.Length) return;

        WaveManagerSpawnDatas enemySpawnDatas = new()
        {
            enemyType = enemyTypes[enemyTypeNumber],
            spawnAmount = startAmount,
        };

        spawningEnemys.Add(enemySpawnDatas);

        enemyTypeNumber++;
    }

    public void OnStartClick()
    {
        StartWave?.Invoke();
        startButton.interactable = false;
        Vector3 spawnPos = MapManager.Instance.StartTilePos;

        StartCoroutine(SpawnEnemys(spawnPos));

        waveCount++;
    }

    private IEnumerator SpawnEnemys(Vector3 spawnPos)
    {
        for (int i = 0; i < spawningEnemys.Count; i++)
        {
            for (int j = 0; j < spawningEnemys[i].spawnAmount; j++)
            {
                GameObject enemy = EnemyPoolingManager.Instance.GetEnemy(spawningEnemys[i].enemyType);
                enemy.transform.position = spawnPos;
                if (enemy.TryGetComponent(out Enemy enemyScript))
                    enemyScript.OnDeathAction += UnRegisterEnemy;
                enemy.SetActive(true);

                currentEnemyAmount++;

                yield return new WaitForSeconds(spawnDelay);
            }
        }
        StartCoroutine(IsWaveFinished());
    }

    private IEnumerator IsWaveFinished()
    {
        yield return new WaitUntil(() =>
            {
                return currentEnemyAmount == 0;
            });

        PrepareNextWave();
    }

    private void PrepareNextWave()
    {
        EndWave?.Invoke();

        for (int i = 0; i < spawningEnemys.Count; i++)
        {
            WaveManagerSpawnDatas data = spawningEnemys[i];
            data.spawnAmount = Mathf.RoundToInt(data.spawnAmount * amountScaling);
            spawningEnemys[i] = data;
        }

        startButton.interactable = true;

        switch (waveCount)
        {
            case 4:
                {
                    AddSpawningEnemy();
                    break;
                }
        }
    }

    private void UnRegisterEnemy()
    {
        currentEnemyAmount--;
    }

    private void OnDisable()
    {
        StartWave = null;
        EndWave = null;
    }
}
