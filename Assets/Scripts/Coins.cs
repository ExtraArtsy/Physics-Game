using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {   
            isCollected = true;
            Debug.Log("Coin collected.");
            CoinManager.instance.ChangeCoins(coinValue);

            Destroy(gameObject);
        }
    }
}
