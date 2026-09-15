using System;
using UnityEngine;

public abstract class TalentUnlockNode : TalentNode
{
    [SerializeField] private TalentCosts cost;

    private void Awake()
    {
        var colors = button.colors;
        colors.normalColor = cost.Currency.Color;
        colors.selectedColor = cost.Currency.Color;
        colors.highlightedColor = cost.Currency.Color;

        button.colors = colors;
    }

    public override void OnLearnTalent(int value)
    {
        if (!CheckCost(value)) return;

        CloseButton();
        ActivateChilds();
    }

    protected override bool CheckCost(int value)
    {
        if (value >= cost.CurrencyAmount) return true;

        return false;
    }
}
