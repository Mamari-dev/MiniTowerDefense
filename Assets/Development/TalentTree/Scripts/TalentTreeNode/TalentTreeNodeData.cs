using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TalentTreeNodeData
{
    [SerializeField] private string guid;
    [SerializeField] private string id;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private int cost;
    [SerializeField] private int maxRangs;
    [SerializeField] private bool unlocked = false;
    [SerializeField] private bool betaLocked;
    [SerializeField] private List<string> childGuids;

    private Vector2 graphPosition;

    public string Guid { get => guid; set => guid = value; }
    public string Id { get => id; set => id = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public int Cost { get => cost; set => cost = value; }
    public int MaxRangs { get => maxRangs; set => maxRangs = value; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public bool BetaLocked { get => betaLocked; set => betaLocked = value; }
    public List<string> ChildGuids { get => childGuids; set => childGuids = value; }

    public Vector2 GraphPosition { get => graphPosition; set => graphPosition = value; }
}
