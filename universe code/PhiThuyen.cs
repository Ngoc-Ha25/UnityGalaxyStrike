using UnityEngine;
using UnityEngine.SceneManagement;

public class PhiThuyen1 : MonoBehaviour
{
    [Header("Điều khiển")]
    public float speed = 10f;            // tốc độ bay
    public float rotationSpeed = 200f;   // tốc độ xoay
    public float tiltAngle = 30f;        // góc nghiêng tối đa (roll)
    public float pitchAngle = 20f;       // góc ngẩng lên/hạ xuống tối đa (pitch)

    [Header("Máu Player")]
    public float maxHealth = 100f;       // máu tối đa
    private float currentHealth;

    [Header("Hiệu ứng nổ")]
    public GameObject explosionEffect;   // prefab hiệu ứng nổ

    [Header("Bắn đạn")]
    public GameObject bulletPrefab;      // prefab đạn
    public Transform firePoint;          // vị trí bắn ra đạn
    public AudioClip shootSound;         // âm thanh bắn
    private AudioSource audioSource;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = false;

        currentHealth = maxHealth;

        // Lấy hoặc thêm AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.spatialBlend = 0f; // âm thanh 2D (luôn nghe rõ)
    }

    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Horizontal");
        float rotateVertical = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotateVertical * rotationSpeed * Time.deltaTime);

        float tilt = -rotateHorizontal * tiltAngle;
        float pitch = -rotateVertical * pitchAngle;

        Quaternion currentRotation = transform.rotation;
        Quaternion tiltRotation = Quaternion.Euler(pitch, currentRotation.eulerAngles.y, tilt);
        transform.rotation = Quaternion.Lerp(currentRotation, tiltRotation, Time.deltaTime * 3f);

        if (Input.GetKey(KeyCode.Space))
        {
            rb.linearVelocity = transform.forward * speed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Bắn khi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Tạo đạn
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        // Phát âm thanh bắn
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage, current health: " + currentHealth);

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        Explode();

        // Hiện màn hình Game Over trước khi hủy object
        GameOverManager gom = FindFirstObjectByType<GameOverManager>();
        if (gom != null)
        {
            gom.ShowGameOver();
        }

        gameObject.SetActive(false);
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }

    // Khi chạm vật thể thắng
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // Vật thể thắng đặt tag là "Finish"
        {
            GameWinManager gwm = FindFirstObjectByType<GameWinManager>();
            if (gwm != null)
            {
                gwm.ShowGameWin();
            }
        }
    }
}