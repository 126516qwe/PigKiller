using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool freezeXZAxis = true; // 只绕Y轴旋转，保持血条水平

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
                // 只让血条面向相机的Y轴旋转
                Vector3 direction = mainCamera.transform.position - transform.position;
                direction.y = 0; // 忽略Y轴差异

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(-direction);
                }
            }
            else
            {
                // 完全面向相机
                transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
            }
        }
    }
}
