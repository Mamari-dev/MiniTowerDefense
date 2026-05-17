using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    private int currenPlayerHealth;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        currenPlayerHealth = playerHealth;
        HealthManager.Instance.SpawnHealth(currenPlayerHealth);
    }

    public void Damage(int damage)
    {
        currenPlayerHealth -= damage;

        CheckPlayerHealth();
    }

    private void CheckPlayerHealth()
    {
        if (currenPlayerHealth <= 0)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
