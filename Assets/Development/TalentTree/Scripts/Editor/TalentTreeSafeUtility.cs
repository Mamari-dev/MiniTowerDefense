using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public static class TalentTreeSafeUtility
{
    public static void SaveGraph(TalentTreeGraphView graphView, TalentTree talentTreeSO)
    {
        if (talentTreeSO == null) return;

        Undo.RecordObject(talentTreeSO, "Save Talent Tree");
        talentTreeSO.Nodes.Clear();

        List<TalentTreeNodeGraph> nodes = graphView.nodes.ToList().Cast<TalentTreeNodeGraph>().ToList();
        List<Edge> edges = graphView.edges.ToList();

        foreach (TalentTreeNodeGraph node in nodes)
        {
            node.nodeData.GraphPosition = node.GetPosition().position;

            node.nodeData.ChildGuids.Clear();

            Port outputPort = node.outputContainer.Q<Port>();
            if (outputPort != null)
            {
                List<Edge> outputEdges = edges.Where(e => e.output == outputPort).ToList();
                foreach(Edge edge in outputEdges)
                {
                    if (edge.input.node is TalentTreeNodeGraph childNode)
                        node.nodeData.ChildGuids.Add(childNode.GUID);
                }
            }

            talentTreeSO.Nodes.Add(node.nodeData);
        }

        EditorUtility.SetDirty(talentTreeSO);
        AssetDatabase.SaveAssets();
    }
}
