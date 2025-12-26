using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    // ==================================================
    // 1. 配置区域
    // ==================================================

    [Header("Configurations")]
    [Tooltip("飞行速度")]
    public float speed = 15f;
    [Tooltip("生命周期（秒）")]
    [SerializeField] private float _lifeTime = 3.0f;
    [Tooltip("回调函数")]
    [SerializeField] private Action<GameObject> _onDeathCallBack;

    // ==================================================
    // 2. 引用区域
    // ==================================================

    private Renderer _renderer;
    private Collider _collider;

    // ==================================================
    // 3. 状态区域
    // ==================================================

    private bool _isFlying = false; // 标记本弹药是否处于飞行状态

    // ==================================================
    // 4. 公共启动接口
    // ==================================================

    public void Init(Action<GameObject> onDeath)
    {
        _onDeathCallBack = onDeath;
        // 每次炮弹被发射（唤醒），要启动确保炮弹的渲染器和碰撞盒处于启动状态
        _renderer.enabled = true;
        _collider.enabled = true;
        // 标记本弹药处于飞行状态
        _isFlying = true;
        // 开启协程让炮弹飞，飞够了时间就调用回调函数
        StartCoroutine(LifeCycleRoutine());
    }

    // ==================================================
    // 5. 生命周期
    // ==================================================

    private void Awake()
    {
        _collider = gameObject.GetComponent<Collider>();
        _renderer = gameObject.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 调试信息
        print($"发生了碰撞！撞击对象: {other.name}, Tag: {other.tag}");

        // 如果该炮弹在激活状态下却不处于飞行状态，说明正在假死，不执行后续逻辑
        if (!_isFlying) return;

        if (other.gameObject.CompareTag("Ground"))
        {
            // 因触碰地面，此炮弹假死
            FakeDeath();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // 调试信息
            print($"{other.name}被碰撞了");

            // 获取被碰撞的敌人所挂载的EnemyController脚本并调用TakeDamage方法
            EnemyController ec = other.GetComponentInParent<EnemyController>();
            if(ec != null)
            {
                ec.TakeDamage();
            }

            // 因碰撞敌人，此炮弹假死
            FakeDeath();
        }
    }

    // ==================================================
    // 6. 私有逻辑方法
    // ==================================================

    private IEnumerator LifeCycleRoutine()
    {
        float timer = 0; // 循环计时器
        while (timer < _lifeTime)
        {
            if( _isFlying)
            {
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }

            timer += Time.deltaTime;
            yield return null;
        }
        _onDeathCallBack?.Invoke(this.gameObject);
    }

    /// <summary>
    /// 弹药“假死”，意为在生命周期_lifeTime内发生碰撞时，
    /// 让碰撞盒和渲染器失效，并且标记_isFlying为false
    /// 从而在游戏中丧失作用，但仍然计算它的生命周期，直至_lifeTime跑满
    /// </summary>
    private void FakeDeath()
    {
        _isFlying = false;
        _collider.enabled = false;
        _renderer.enabled = false;
    }
}
