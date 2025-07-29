using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // 따라갈 오브젝트
    public Vector3 offset = new Vector3(0, 0, -10f); // 카메라와 타겟의 거리
    public float smoothSpeed = 5f; // 부드럽게 따라가는 속도

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
