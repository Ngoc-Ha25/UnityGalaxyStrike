using System.Collections;
using UnityEngine;

public class TauLon1 : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    private bool canFly = true;

    public Transform player;          // tham chiếu đến Player
    public AudioClip approachSound;   // âm thanh khi tới gần
    private AudioSource audioSource;
    public float triggerDistance = 15f; // khoảng cách để phát âm thanh
    private bool hasPlayed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = false;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 1f; // âm thanh 3D

        StartCoroutine(StopAndGo());
    }

    void Update()
    {
        if (canFly)
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Kiểm tra khoảng cách tới Player
        if (player != null && !hasPlayed)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= triggerDistance)
            {
                audioSource.PlayOneShot(approachSound);
                hasPlayed = true; // chỉ phát một lần
            }
        }
    }

    IEnumerator StopAndGo()
    {
        canFly = false;
        yield return new WaitForSeconds(1f);
        canFly = true;
    }
}