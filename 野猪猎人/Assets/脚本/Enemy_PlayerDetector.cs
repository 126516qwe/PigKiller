using UnityEngine;

public class Enemy_PlayerDetector : MonoBehaviour
{
    [Header("检测设置")]
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private bool playerInRange = false;
    private Transform detectedPlayer;
    private SphereCollider detectCollider;

    void Awake()
    {
        detectCollider = gameObject.GetComponent<SphereCollider>();
        if (detectCollider == null)
        {
            detectCollider = gameObject.AddComponent<SphereCollider>();
            detectCollider.isTrigger = true;
        }
        detectCollider.radius = detectRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            playerInRange = true;
            detectedPlayer = other.transform;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // 确保始终更新玩家位置
            detectedPlayer = other.transform;
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && detectedPlayer == other.transform)
        {
            playerInRange = false;
            detectedPlayer = null;
        }
    }

    // 新增：重生时强制重新检测
    public void OnRespawn()
    {
        // 重置状态
        playerInRange = false;
        detectedPlayer = null;

        // 手动检测范围内的玩家
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, detectRange);
        foreach (Collider collider in collidersInRange)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                playerInRange = true;
                detectedPlayer = player.transform;
                break; // 找到一个玩家就足够
            }
        }
    }

    public Transform GetPlayerTransform()
    {
        return detectedPlayer;
    }

    public bool IsPlayerInRange()
    {
        return playerInRange && detectedPlayer != null;
    }

    public void TurnOffPlayerInRange()
    {
        playerInRange = false;
        detectedPlayer = null;
    }

    // 可视化检测范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = playerInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}