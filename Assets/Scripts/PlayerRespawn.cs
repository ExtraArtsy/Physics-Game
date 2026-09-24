using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform currentCheckpoint;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (currentCheckpoint == null)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        if(currentCheckpoint != null)
        {
            transform.position = currentCheckpoint.position;
            if(rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hazard"))
        {
            Respawn();
        }
        
        if(collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform;
        }
    }
}
