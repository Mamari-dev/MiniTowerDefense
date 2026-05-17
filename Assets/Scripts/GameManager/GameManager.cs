using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerHealth;
    private int currenPlayerHealth;

    [SerializeField] private string deathSceneName;

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
            SceneManager.LoadScene(deathSceneName, LoadSceneMode.Additive); //2 = index for deathscene in buildsettings
        }
    }
}
