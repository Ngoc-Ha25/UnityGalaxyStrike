using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public float maxHealth = 50f;
    private float currentHealth;

    public Transform player;
    public GameObject explosionEffect; // prefab hiệu ứng nổ
    public float damageToPlayer = 20f; // sát thương gây cho player

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Enemy took damage, current health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Explode();
        Destroy(gameObject);
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;   // dùng velocity
            transform.forward = direction;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PhiThuyen1 ship = collision.gameObject.GetComponent<PhiThuyen1>();
            if (ship != null)
            {
                ship.TakeDamage(damageToPlayer);
            }

            Explode();
            Destroy(gameObject);
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}