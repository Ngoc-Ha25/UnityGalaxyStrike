using UnityEngine;

public class TauLon : MonoBehaviour
{
    public float speed = 5f;   // tốc độ bay

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false; // không bị rơi
        rb.isKinematic = false;
    }

    void Update()
    {
        // Luôn bay thẳng về phía trước
        rb.linearVelocity = transform.forward * speed;
    }
}