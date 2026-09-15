using System;
using UnityEngine;

[Serializable]
public struct TalentCosts
{
    [SerializeField] private Currency currency;
    [SerializeField] private int currencyAmount;

    public Currency Currency { get => currency; set => currency = value; }
    public int CurrencyAmount { get => currencyAmount; set => currencyAmount = value; }
}
