using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private TowerStats towerStats;
    protected TowerStats towerRunTimeStats;
    [SerializeField] private CircleCollider2D towerCollider;
    [SerializeField] private LayerMask enemyLayer;
    private List<Transform> enemys = new();

    #region Editor
    public TowerStats TowerStats { get => towerStats; }
    public TowerStats TowerRunTimeStats { get => towerRunTimeStats; set => towerRunTimeStats = value; }

    [HideInInspector] public bool foldout;
    #endregion

    private void Start()
    {
        towerRunTimeStats = Instantiate(towerStats);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            enemys.Add(collision.transform);

            if (enemys.Count == 1)
                StartCoroutine(AttackCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            enemys.Remove(collision.transform);

            if (enemys.Count == 0)
                StopAllCoroutines();
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (enemys.Count > 0)
        {
            Transform frontEnemyTransform = enemys[0];
            Shoot(frontEnemyTransform);

            yield return new WaitForSeconds(towerRunTimeStats.attackSpeed);
        }
    }

    protected abstract void Shoot(Transform frontEnemyTransform);
}
