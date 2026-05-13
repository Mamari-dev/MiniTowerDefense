using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //wavemanager?
    [SerializeField] private EnemyType[] enemyTypes;

    [ContextMenu("Spwan Enemy")]
    private void SpawnEnemy()
    {
        Vector3 spawnPos = MapManager.Instance.StartTilePos;

        GameObject enemy = EnemyPoolingManager.Instance.GetEnemy(enemyTypes[0]);
        enemy.transform.position = spawnPos;
        enemy.SetActive(true);
    }
}
