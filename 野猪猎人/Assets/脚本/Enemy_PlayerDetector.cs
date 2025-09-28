using UnityEngine;

public class Enemy_PlayerDetector : MonoBehaviour
{
    [Header("�������")]
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
            // ȷ��ʼ�ո������λ��
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

    // ����������ʱǿ�����¼��
    public void OnRespawn()
    {
        // ����״̬
        playerInRange = false;
        detectedPlayer = null;

        // �ֶ���ⷶΧ�ڵ����
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, detectRange);
        foreach (Collider collider in collidersInRange)
        {
            Player player = collider.GetComponent<Player>();
            if (player != null)
            {
                playerInRange = true;
                detectedPlayer = player.transform;
                break; // �ҵ�һ����Ҿ��㹻
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

    // ���ӻ���ⷶΧ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = playerInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}