using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TalentTree", menuName = "Scriptable Objects/TalentTree/TalentTree")]
public class TalentTree : ScriptableObject
{
    [SerializeField] private List<TalentTreeNodeData> nodes;

    public List<TalentTreeNodeData> Nodes { get => nodes; private set => nodes = value; }
}
