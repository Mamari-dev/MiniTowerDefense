using System;

[Serializable]
public struct WaveManagerEnemyDatas
{
    public EnemyType EnemyType;
    public int SpawnAmount;
    public float SpawnDelay;

    public float SpawnAmountScaling;
    public float SpawnDelayScaling;
    public float HpScaling;
}
