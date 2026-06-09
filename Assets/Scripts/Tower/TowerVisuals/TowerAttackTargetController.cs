using UnityEngine;

public class TowerAttackTargetController : TowerAttackController
{
    [SerializeField] private TowerAttackTargetStruct[] attackTargetButtons;

    protected override void SetAttackTarget()
    {
        if (towerCombatShootingStats == null) return;
        foreach (var targetStruct in attackTargetButtons)
        {
            if (targetStruct.button == lastClickedButton)
            {
                towerCombatShootingStats.attackTargetTypes = targetStruct.target;
                break;
            }
        }
    }

    protected override void InitStartColor()
    {
        foreach (var targetStruct in attackTargetButtons)
        {
            if (targetStruct.target == towerCombatShootingStats.attackTargetTypes)
            {
                ChangeButtonColor(targetStruct.button, clickedTypeButtonClickedColor);
                lastClickedButton = targetStruct.button;
                break;
            }
        }
    }
}
