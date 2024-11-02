using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;  // Đối tượng mà camera sẽ theo dõi (nhân vật của bạn)
    public float smoothSpeed = 0.125f;  // Tốc độ mượt mà của camera
    public Vector3 offset;  // Độ lệch của camera so với vị trí của đối tượng

    private void LateUpdate()
    {
        if (target != null)
        {
            // Tính toán vị trí mong muốn của camera
            Vector3 desiredPosition = target.position + offset;

            // Nội suy từ vị trí hiện tại đến vị trí mong muốn, giúp camera di chuyển mượt mà
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Gán vị trí của camera thành vị trí đã nội suy
            transform.position = smoothedPosition;
        }
    }
}
