using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TalentTreeNodeGraph : Node
{
    public string GUID;
    public TalentTreeNodeData nodeData;

    public TalentTreeNodeGraph(string title, TalentTreeNodeData initialData = null)
    {
        GUID = initialData != null ? initialData.Guid : System.Guid.NewGuid().ToString();
        nodeData = initialData ?? new TalentTreeNodeData()
        {
            Guid = GUID,
            Id = title
        };

        this.title = string.IsNullOrEmpty(nodeData.Id) ? title : nodeData.Id; ;

        AddPorts();
        VisualElement customContainer = CreateDataContainer();
        SetNodeName(customContainer);
        SetNodeIcon(customContainer);
        SetNodeDescription(customContainer);
        SetNodeCost(customContainer);
        SetNodeMaxRang(customContainer);
        SetNodeUnlockToggle(customContainer);
        SetNodeBetaToggle(customContainer);


        style.width = 200;
        //style.height = 100;

        mainContainer.Add(customContainer);

        RefreshExpandedState();
        RefreshPorts();
    }

    private void AddPorts()
    {
        Port inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
        inputPort.portName = "parent";
        inputContainer.Add(inputPort);

        Port outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        outputPort.portName = "childs";
        outputContainer.Add(outputPort);
    }

    private VisualElement CreateDataContainer()
    {
        VisualElement customContainer = new VisualElement();
        customContainer.style.paddingTop = 5;
        customContainer.style.paddingBottom = 5;

        return customContainer;
    }

    private void SetNodeName(VisualElement customContainer)
    {
        TextField idField = new TextField("ID") { value = nodeData.Id };
        idField.RegisterValueChangedCallback(evt =>
        {
            nodeData.Id = evt.newValue;
            this.title = evt.newValue; // Live Header Update
        });
        customContainer.Add(idField);
    }

    private void SetNodeIcon(VisualElement customContainer)
    {
        ObjectField iconField = new ObjectField("Icon")
        {
            objectType = typeof(Sprite),
            value = nodeData.Icon
        };
        iconField.RegisterValueChangedCallback(evt =>
        {
            nodeData.Icon = evt.newValue as Sprite;
        });
        customContainer.Add(iconField);
    }

    private void SetNodeDescription(VisualElement customContainer)
    {
        TextField descField = new TextField("Description")
        {
            value = nodeData.Description,
            multiline = true
        };
        descField.RegisterValueChangedCallback(evt =>
        {
            nodeData.Description = evt.newValue;
        });
        customContainer.Add(descField);
    }

    private void SetNodeCost(VisualElement customContainer)
    {
        IntegerField costField = new IntegerField("Cost") { value = nodeData.Cost };
        costField.RegisterValueChangedCallback(evt =>
        {
            nodeData.Cost = evt.newValue;
        });
        customContainer.Add(costField);
    }

    private void SetNodeMaxRang(VisualElement customContainer)
    {
        IntegerField maxRangsField = new IntegerField("Max Rangs") { value = nodeData.MaxRangs };
        maxRangsField.RegisterValueChangedCallback(evt =>
        {
            nodeData.MaxRangs = evt.newValue;
        });
        customContainer.Add(maxRangsField);
    }

    private void SetNodeUnlockToggle(VisualElement customContainer)
    {
        Toggle unlockedToggle = new Toggle("Unlocked") { value = nodeData.Unlocked };
        unlockedToggle.RegisterValueChangedCallback(evt =>
        {
            nodeData.Unlocked = evt.newValue;
        });
        customContainer.Add(unlockedToggle);
    }

    private void SetNodeBetaToggle(VisualElement customContainer)
    {
        Toggle betaLockedToggle = new Toggle("Beta Locked") { value = nodeData.BetaLocked };
        betaLockedToggle.RegisterValueChangedCallback(evt =>
        {
            nodeData.BetaLocked = evt.newValue;
        });
        customContainer.Add(betaLockedToggle);
    }
}
