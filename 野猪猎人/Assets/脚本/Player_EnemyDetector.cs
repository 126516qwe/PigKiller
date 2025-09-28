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

            // ���ĵ��˵������¼�
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

            // ȡ�������¼�
            enemy.health.OnDeath -= OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        // �Ƴ������������ĵ���
        enemiesInRange.RemoveAll(enemy => enemy.health.isDead);
        UpdateEnemyInRangeStatus();
    }

    private void UpdateEnemyInRangeStatus()
    {
        // ����б����Ƿ��д��ĵ���
        enemyInRange = enemiesInRange.Any(enemy => !enemy.health.isDead);

        // ���߸���ȷ��д����
        // enemyInRange = enemiesInRange.Count > 0 && enemiesInRange.Any(enemy => !enemy.IsDead);
    }

    // �������������ڴ�й©
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
