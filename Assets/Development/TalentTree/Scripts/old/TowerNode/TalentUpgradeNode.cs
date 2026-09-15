using UnityEngine;

public abstract class TalentUpgradeNode : TalentNode
{
    [SerializeField] private int currentLevel = 0;
    [SerializeField] private bool isLearnd;
    [SerializeField] private TalentCosts[] costs;

    private void Awake()
    {
        UpgradeButtonColor();
    }

    public override void OnLearnTalent(int value)
    {
        if (!CheckCost(value)) return;

        if (!isLearnd)
            ActivateChilds();

        isLearnd = true;
        currentLevel++;
        if (currentLevel == costs.Length)
            CloseButton();
        else
        {
            UpgradeButtonColor();
            Upgrade();
        }
    }

    protected override bool CheckCost(int value)
    {
        if (value >= costs[currentLevel].CurrencyAmount) return true;

        return false;
    }

    private void UpgradeButtonColor()
    {
        var colors = button.colors;
        colors.normalColor = costs[currentLevel].Currency.Color;
        colors.selectedColor = costs[currentLevel].Currency.Color;
        colors.highlightedColor = costs[currentLevel].Currency.Color;

        button.colors = colors;
    }

    protected abstract void Upgrade();
}
