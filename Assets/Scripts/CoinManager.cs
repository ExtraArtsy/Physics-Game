using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Goal Settings")]
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalSpawnPoint;


    private int currentCoins = 0;
    private int totalCoins = 0;
    private bool goalSpawned = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalCoins = FindObjectsByType<Coins>(FindObjectsSortMode.None).Length;
        UpdateUI();
    }

    public void ChangeCoins(int amount)
    {
        currentCoins += amount;
        UpdateUI();

        CheckWinCondition();
    }

    private void UpdateUI()
    {
        coinText.text = "Coins: " + currentCoins.ToString() + " / " + totalCoins.ToString();
    }

    private void CheckWinCondition()
    {
        if(currentCoins >= totalCoins && !goalSpawned)
        {
            goalSpawned = true;
            SpawnGoal();
        }
    }

    private void SpawnGoal()
    {
        if(goalPrefab != null && goalSpawnPoint != null)
        {
            Instantiate(goalPrefab, goalSpawnPoint.position, goalSpawnPoint.rotation);
            Debug.Log("All coins collected! Goal spawned.");
        }
        else
        {
            Debug.LogWarning("Goal Prefab or Spawn Point is not assigned.");
        }
    }
}
