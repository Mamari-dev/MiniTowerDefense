using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TalentNode : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] private List<TalentNode> childs;

    public abstract void OnLearnTalent(int value);

    protected abstract bool CheckCost(int value);

    protected void CloseButton()
    {
        button.interactable = false;
    }

    protected void ActivateChilds()
    {
        if (childs.Count == 0) return;

        foreach (TalentNode child in childs)
        {
            child.gameObject.SetActive(true);
        }
    }
}
