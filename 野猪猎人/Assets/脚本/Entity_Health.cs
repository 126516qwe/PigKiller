using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
public class Entity_Health : MonoBehaviour
{
    public event System.Action OnDeath;
    public event System.Action<float> OnDamageTaken;

    [SerializeField]protected float currentHP;
    [SerializeField]protected float maxHP;

    private Slider healthBar;
    public bool isDead { get; protected set; }

    protected virtual void Awake()
    {
        isDead = false;
        currentHP = maxHP;

        healthBar = GetComponentInChildren<Slider>();

        UpdateHealthBar();
    }
    public virtual void TakeDamage(float damage)
    {
        if(isDead)
            return;

        ReduceHp(damage);

        OnDamageTaken?.Invoke(damage);

        if (currentHP <= 0)
        {
            currentHP = 0;
            OnDeath?.Invoke(); // ´¥·¢ËÀÍöÊÂ¼þ
        }

    }

    protected virtual void ReduceHp(float damage)
    {
        currentHP-=damage;
        UpdateHealthBar();

    }

    public virtual void Die()
    {
        isDead = true;
    }

    protected void UpdateHealthBar() => healthBar.value = currentHP / maxHP;
}
