using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerBaseStats towerBaseStats;
    protected TowerBaseStats towerRunTimeBaseStats;
    
    #region Editor
    public TowerBaseStats TowerBaseStats { get => towerBaseStats; }
    public TowerBaseStats TowerRunTimeBaseStats { get => towerRunTimeBaseStats; }

    [HideInInspector] public bool foldout;
    #endregion

    protected virtual void Awake()
    {
        towerRunTimeBaseStats = Instantiate(towerBaseStats);
    }
}
