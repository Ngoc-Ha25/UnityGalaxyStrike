using UnityEngine;

public class Xoay : MonoBehaviour
{
    public Transform wheel;          // bánh xe cần xoay
    public float rotationSpeed = 200f; // tốc độ xoay

    void Update()
    {
        if (wheel != null)
        {
            // xoay theo chiều kim đồng hồ quanh trục Z
            wheel.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        }
    }
}