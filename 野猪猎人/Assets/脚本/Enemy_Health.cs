using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health : Entity_Health
{
   
    protected Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }
    public override void Die()
    {
        base.Die();
        enemy.enemyPlayerDetector.TurnOffPlayerInRange();
    }
    public virtual void Relife()
    {
        isDead = false;
        currentHP = maxHP;

        Vector3 randomOffset = new Vector3(Random.Range(-15f, 15f),0f,Random.Range(-15f, 15f));

        transform.position = transform.position + randomOffset;

        transform.rotation = Quaternion.identity;

        UpdateHealthBar();
        enemy.enemyPlayerDetector.OnRespawn();
    }  
}
