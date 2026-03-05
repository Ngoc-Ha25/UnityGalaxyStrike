using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;        // tham chiếu đến phi thuyền
    public Vector3 offset = new Vector3(0f, 5f, -10f); // khoảng cách camera so với player
    public float followSpeed = 5f;  // tốc độ theo dõi

    void LateUpdate()
    {
        if (player != null)
        {
            // vị trí mong muốn của camera
            Vector3 targetPosition = player.position + player.TransformDirection(offset);

            // di chuyển camera mượt mà
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            // luôn nhìn vào player
            transform.LookAt(player);
        }
    }
}