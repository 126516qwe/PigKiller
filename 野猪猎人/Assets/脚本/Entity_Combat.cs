using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private BoxCollider _box;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask targetLayers;

    // ��ӹ�������ݱ�ʶ
    [SerializeField] private bool isPlayerAttack; // true=��ҹ���, false=���˹���
    private EntityType attackerType; // ��רҵ��ö�ٷ�ʽ

    private void Awake()
    {
        _box = GetComponent<BoxCollider>();
        _box.enabled = false;

        // �Զ���⹥�������
        AutoDetectAttackerType();
    }

    private void AutoDetectAttackerType()
    {
        // ����1��ͨ���������������
        if (GetComponent<Player>() != null || GetComponentInParent<Player>() != null)
        {
            isPlayerAttack = true;
            attackerType = EntityType.Player;
        }
        else if (GetComponent<Enemy>() != null || GetComponentInParent<Enemy>() != null)
        {
            isPlayerAttack = false;
            attackerType = EntityType.Enemy;
        }
    }

    public void OpenAttackTrigger()
    {
        _box.enabled = true;
    }

    public void CloseAttackTrigger()
    {
        _box.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ʹ��LayerMask����Ŀ��
        if (((1 << other.gameObject.layer) & targetLayers) == 0)
            return;

        // ���ݹ�������ݴ����˺�
        if (isPlayerAttack)
        {
            // ��ҹ�����ֻ�˺�����
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health.TakeDamage(damage);
                return;
            }
        }
        else
        {
            // ���˹�����ֻ�˺����
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.health.TakeDamage(damage);
                return;
            }
        }
    }

    // ���ù��������͵ķ���
    public void SetAttacker(bool isPlayerAttacker)
    {
        isPlayerAttack = isPlayerAttacker;
    }

    public void SetAttackerType(EntityType type)
    {
        attackerType = type;
        isPlayerAttack = (type == EntityType.Player);
    }
}

// ö�ٶ��幥��������
public enum EntityType
{
    Player,
    Enemy,
    Neutral
}