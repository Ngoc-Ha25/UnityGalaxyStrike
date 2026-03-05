using UnityEngine;

public class XoayY : MonoBehaviour
{
    public Transform wheel;            // bánh xe cần xoay
    public float rotationSpeed = 200f; // tốc độ xoay

    void Update()
    {
        if (wheel != null)
        {
            // xoay ngang quanh trục Y
            wheel.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}