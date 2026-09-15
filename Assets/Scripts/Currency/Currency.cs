using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyData", menuName = "Scriptable Objects/Currency")]
public class Currency : ScriptableObject
{
    [SerializeField] private CurrencyTypes type;
    [SerializeField] private Color color;

    public CurrencyTypes Type { get => type; set => type = value; }
    public Color Color { get => color; set => color = value; }
}
