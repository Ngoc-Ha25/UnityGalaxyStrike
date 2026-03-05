using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;       // điểm bắn (Empty đặt ở mũi phi thuyền)
    public GameObject bulletPrefab;   // prefab viên đạn
    public float bulletSpeed = 30f;   // tốc độ bay của đạn
    public float bulletLife = 3f;     // thời gian tồn tại của đạn

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Nhấn chuột trái để bắn
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Ray từ camera tới vị trí chuột
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetDirection;

        // Nếu raycast trúng vật thể trong scene
        if (Physics.Raycast(ray, out hit))
        {
            targetDirection = (hit.point - firePoint.position).normalized;
        }
        else
        {
            // Nếu không trúng gì thì bắn theo hướng ray
            targetDirection = ray.direction;
        }

        // Tạo viên đạn tại firePoint
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(targetDirection));

        // Cho viên đạn bay về phía chuột
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = targetDirection * bulletSpeed; // ✅ dùng velocity
        }

        // Xoá viên đạn sau một khoảng thời gian
        Destroy(bullet, bulletLife);
    }
}