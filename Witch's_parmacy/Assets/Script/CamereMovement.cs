using UnityEngine;

public class CamereMovement : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ�
    public Vector3 offset; // ī�޶�� �÷��̾� ������ �Ÿ�

    void LateUpdate()
    {
        if (player != null)
        {
            // ��ǥ ��ġ ���
            Vector3 player2Dposition = new Vector3(player.position.x, player.position.y, -10);
            Vector3 desiredPosition = player2Dposition + offset;

            // �ε巴�� �̵�
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ī�޶� ��ġ ������Ʈ
            transform.position = smoothedPosition;
        }
    }
}
