using UnityEngine;
using UnityEngine.UI;

public class WaveManagerActionButton : WaveManagerAction
{
    [SerializeField] private Button button;
    protected override void DisableObject()
    {
        button.interactable = false;
    }

    protected override void EnableObject()
    {
        button.interactable = true;
    }
}
