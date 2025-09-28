using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    private BoxCollider _box;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask targetLayers;

    // 添加攻击者身份标识
    [SerializeField] private bool isPlayerAttack; // true=玩家攻击, false=敌人攻击
    private EntityType attackerType; // 更专业的枚举方式

    private void Awake()
    {
        _box = GetComponent<BoxCollider>();
        _box.enabled = false;

        // 自动检测攻击者身份
        AutoDetectAttackerType();
    }

    private void AutoDetectAttackerType()
    {
        // 方法1：通过父物体或自身检测
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
        // 使用LayerMask过滤目标
        if (((1 << other.gameObject.layer) & targetLayers) == 0)
            return;

        // 根据攻击者身份处理伤害
        if (isPlayerAttack)
        {
            // 玩家攻击：只伤害敌人
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.health.TakeDamage(damage);
                return;
            }
        }
        else
        {
            // 敌人攻击：只伤害玩家
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.health.TakeDamage(damage);
                return;
            }
        }
    }

    // 设置攻击者类型的方法
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

// 枚举定义攻击者类型
public enum EntityType
{
    Player,
    Enemy,
    Neutral
}