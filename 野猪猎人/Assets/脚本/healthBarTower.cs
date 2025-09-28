using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool freezeXZAxis = true; // ֻ��Y����ת������Ѫ��ˮƽ

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            if (freezeXZAxis)
            {
                // ֻ��Ѫ�����������Y����ת
                Vector3 direction = mainCamera.transform.position - transform.position;
                direction.y = 0; // ����Y�����

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(-direction);
                }
            }
            else
            {
                // ��ȫ�������
                transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
            }
        }
    }
}
