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
            Vector3 desiredPosition = target.position + offset;
            float t = 1f - Mathf.Pow(1f - Mathf.Clamp01(smoothSpeed), Time.deltaTime * 60f);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, t);
            transform.position = smoothedPosition;
        }
    }
}
