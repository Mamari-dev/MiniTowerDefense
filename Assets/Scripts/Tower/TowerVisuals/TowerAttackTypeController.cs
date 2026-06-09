using UnityEngine;

public class TowerAttackTypeController : TowerAttackController
{
    [SerializeField] private TowerAttackTypeStruct[] attackTypeButtons;

    protected override void SetAttackTarget()
    {
        if (towerCombatShootingStats == null) return;
        foreach (var typeStruct in attackTypeButtons)
        {
            if (typeStruct.button == lastClickedButton)
            {
                towerCombatShootingStats.attackPattern = typeStruct.type;
                break;
            }
        }
    }

    protected override void InitStartColor()
    {
        foreach (var typeStruct in attackTypeButtons)
        {
            if (typeStruct.type == towerCombatShootingStats.attackPattern)
            {
                ChangeButtonColor(typeStruct.button, clickedTypeButtonClickedColor);
                lastClickedButton = typeStruct.button;
                break;
            }
        }
    }
}
