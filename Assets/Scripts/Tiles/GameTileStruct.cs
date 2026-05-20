using System;
using UnityEngine;

[Serializable]
public struct GameTileStruct
{
    [HideInInspector] public GameObject tower;
    [HideInInspector] public TowerTypes towerType;
    public bool isPath;
    public bool isBuildable;
}
