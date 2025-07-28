using UnityEngine;

public class CamereMovement : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public float smoothSpeed = 0.125f; // 카메라 이동 속도
    public Vector3 offset; // 카메라와 플레이어 사이의 거리

    void LateUpdate()
    {
        if (player != null)
        {
            // 목표 위치 계산
            Vector3 player2Dposition = new Vector3(player.position.x, player.position.y, -10);
            Vector3 desiredPosition = player2Dposition + offset;

            // 부드럽게 이동
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 카메라 위치 업데이트
            transform.position = smoothedPosition;
        }
    }
}
