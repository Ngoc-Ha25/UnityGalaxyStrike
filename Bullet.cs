using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f; // lượng sát thương gây ra

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject); // hủy viên đạn sau khi trúng enemy
        }
    }
}