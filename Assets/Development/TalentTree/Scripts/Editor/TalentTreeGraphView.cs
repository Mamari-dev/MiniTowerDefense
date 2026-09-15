using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class TalentTreeGraphView : GraphView
{
    public TalentTreeGraphView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        GridBackground grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        RegisterCallback<ContextualMenuPopulateEvent>(OnContextMenuPopulate);

        graphViewChanged = OnGraphViewChanged;
    }

    private void OnContextMenuPopulate(ContextualMenuPopulateEvent evt)
    {
        Vector2 mousePos = evt.localMousePosition;
        evt.menu.AppendAction("Node erstellen", action => AddNode("Talent", mousePos));
    }

    public void AddNode(string nodeName, Vector2 position)
    {
        TalentTreeNodeGraph node = new TalentTreeNodeGraph(nodeName);
        node.SetPosition(new Rect(position, new Vector2(200, 200)));
        AddElement(node);
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();
        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node && startPort.direction != port.direction)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange change)
    {
        if (change.edgesToCreate != null)
        {
            for (int i = change.edgesToCreate.Count - 1; i >= 0; i--)
            {
                Edge edge = change.edgesToCreate[i];
                TalentTreeNodeGraph parentNode = edge.output.node as TalentTreeNodeGraph;
                TalentTreeNodeGraph childNode = edge.input.node as TalentTreeNodeGraph;

                // Wenn das Verbinden einen Zyklus erzeugen würde -> Breche die Erstellung ab!
                if (IsAncestor(parentNode, childNode))
                {
                    change.edgesToCreate.RemoveAt(i);
                }
            }
        }
        return change;
    }

    private bool IsAncestor(TalentTreeNodeGraph node, TalentTreeNodeGraph potentialAncestor)
    {
        if (node == null || potentialAncestor == null) return false;
        if (node == potentialAncestor) return true;

        Port inputPort = node.inputContainer.Q<Port>();
        if (inputPort == null) return false;

        foreach (Edge edge in inputPort.connections)
        {
            TalentTreeNodeGraph parentNode = edge.output.node as TalentTreeNodeGraph;
            if (parentNode != null)
                if (IsAncestor(parentNode, potentialAncestor)) return true;
        }

        return false;
    }
}
