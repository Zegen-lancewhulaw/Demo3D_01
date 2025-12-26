using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // ==================================================
    // 1. 配置区域
    // ==================================================
    [Header("Attributes")]
    [Tooltip("最大血量")]
    [SerializeField] private int maxHealth = 3;

    // ==================================================
    // 2. 状态区域
    // ==================================================
    private int _currentHealth;

    // ==================================================
    // 3. 公共接口
    // ==================================================

    /// <summary>
    /// 搭载此脚本的游戏对象（Enemy）提供给外部的受伤接口
    /// </summary>
    public void TakeDamage()
    {
        _currentHealth--;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    // ==================================================
    // 4. 生命周期
    // ==================================================

    void Awake()
    {
        _currentHealth = maxHealth;
    }

    // ==================================================
    // 5. 私有逻辑方法
    // ==================================================

    void Die()
    {
        GameObject.Destroy(this.gameObject);
    }
}
