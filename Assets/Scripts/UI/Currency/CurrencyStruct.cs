using System;
using UnityEngine;

[Serializable]
public struct CurrencyStruct
{
    public CurrencyTypes Type;
    public Sprite Sprite;
    public Color Color;
    public int Amount;
    public int MaxAmount;
}
