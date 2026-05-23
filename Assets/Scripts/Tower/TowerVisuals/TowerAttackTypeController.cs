using UnityEngine;

public class TowerAttackTypeController : TowerAttackController
{
    [SerializeField] private TowerAttackTypeStruct[] attackTypeButtons;

    protected override void SetAttackTarget()
    {
        foreach (var typeStruct in attackTypeButtons)
        {
            if (typeStruct.button == lastClickedButton)
            {
                towerCombatStats.attackPattern = typeStruct.type;
                break;
            }
        }
    }

    protected override void InitStartColor()
    {
        foreach (var typeStruct in attackTypeButtons)
        {
            if (typeStruct.type == towerCombatStats.attackPattern)
            {
                ChangeButtonColor(typeStruct.button, clickedTypeButtonClickedColor);
                lastClickedButton = typeStruct.button;
                break;
            }
        }
    }
}
