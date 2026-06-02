using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private WaveManagerEnemyDatas[] enemyDatas;
    [SerializeField] private float waveDelayScaling = 0.85f;
    private int waveCount = 0;
    private int currentEnemyAmount = 0;
    private int currentSpawnerAmount = 0;

    private List<WaveManagerEnemyDatas> spawningEnemys = new();

    public static Action StartWave;
    public static Action EndWave;

    private void Start()
    {
        AddSpawningEnemy(EnemyType.normal);
    }

    private void AddSpawningEnemy(EnemyType type)
    {
        foreach (var enemyData in enemyDatas)
        {
            if (enemyData.EnemyType == type)
            {
                spawningEnemys.Add(enemyData);
            }
        }
    }

    public void OnStartClick()
    {
        StartWave?.Invoke();
        startButton.interactable = false;
        Vector3 spawnPos = MapManager.Instance.StartTilePos;

        waveCount++;

        StartCoroutine(SpawnEnemyWaves(spawnPos));
    }

    private IEnumerator SpawnEnemyWaves(Vector3 spawnPos)
    {
        for (int i = 0; i < spawningEnemys.Count; i++)
        {
            float newWaveDelay = spawningEnemys[i].SpawnAmount * waveDelayScaling;

            currentSpawnerAmount++;
            StartCoroutine(SpawnEnemys(spawnPos, spawningEnemys[i]));
            yield return new WaitForSeconds(newWaveDelay);
        }

        StartCoroutine(IsWaveFinished());
    }

    private IEnumerator SpawnEnemys(Vector3 spawnPos, WaveManagerEnemyDatas enemyData)
    {
        for (int j = 0; j < enemyData.SpawnAmount; j++)
        {
            GameObject enemy = EnemyPoolingManager.Instance.GetEnemy(enemyData.EnemyType);
            enemy.transform.position = spawnPos;
            if (enemy.TryGetComponent(out Enemy enemyScript))
            {
                enemyScript.OnDeathAction += UnRegisterEnemy;
                enemyScript.InitStats(enemyData.HpScaling, waveCount);
            }
            enemy.SetActive(true);

            currentEnemyAmount++;

            yield return new WaitForSeconds(enemyData.SpawnDelay);
        }

        currentSpawnerAmount--;
    }

    private IEnumerator IsWaveFinished()
    {
        yield return new WaitUntil(() =>
            {
                return currentSpawnerAmount == 0 && currentEnemyAmount == 0;
            });

        PrepareNextWave();
    }

    private void PrepareNextWave()
    {
        EndWave?.Invoke();

        for (int i = 0; i < spawningEnemys.Count; i++)
        {
            WaveManagerEnemyDatas data = spawningEnemys[i];
            data.SpawnAmount = Mathf.RoundToInt(data.SpawnAmount * data.SpawnAmountScaling);
            spawningEnemys[i] = data;
        }

        startButton.interactable = true;

        switch (waveCount)
        {
            case 4:
                {
                    AddSpawningEnemy(EnemyType.speed);
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
