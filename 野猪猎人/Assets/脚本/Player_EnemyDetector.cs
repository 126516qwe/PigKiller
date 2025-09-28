using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_EnemyDetector : MonoBehaviour
{
    private List<Enemy> enemiesInRange = new List<Enemy>();
    private bool enemyInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Add(enemy);
            UpdateEnemyInRangeStatus();

            // 订阅敌人的死亡事件
            enemy.health.OnDeath += OnEnemyDeath;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemiesInRange.Contains(enemy))
        {
            enemiesInRange.Remove(enemy);
            UpdateEnemyInRangeStatus();

            // 取消订阅事件
            enemy.health.OnDeath -= OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        // 移除所有已死亡的敌人
        enemiesInRange.RemoveAll(enemy => enemy.health.isDead);
        UpdateEnemyInRangeStatus();
    }

    private void UpdateEnemyInRangeStatus()
    {
        // 检查列表中是否有存活的敌人
        enemyInRange = enemiesInRange.Any(enemy => !enemy.health.isDead);

        // 或者更明确的写法：
        // enemyInRange = enemiesInRange.Count > 0 && enemiesInRange.Any(enemy => !enemy.IsDead);
    }

    // 清理方法，避免内存泄漏
    private void OnDestroy()
    {
        foreach (var enemy in enemiesInRange)
        {
            if (enemy != null && enemy.health != null)
                enemy.health.OnDeath -= OnEnemyDeath;
        }
    }
    public bool IsEnemyInRange()
    {
        return enemyInRange;
    }
}
