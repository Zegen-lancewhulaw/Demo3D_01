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
    // 2. 公共启动接口
    // ==================================================

    public void Init(Action<GameObject> onDeath)
    {
        _onDeathCallBack = onDeath;
        // 开启协程让炮弹飞，飞够了时间就调用回调函数
        StartCoroutine(LifeCycleRoutine());
    }

    // ==================================================
    // 3. 私有逻辑方法
    // ==================================================

    private IEnumerator LifeCycleRoutine()
    {
        float timer = 0; // 循环计时器
        while (timer < _lifeTime)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            timer += Time.deltaTime;
            yield return null;
        }
        _onDeathCallBack?.Invoke(this.gameObject);
    }
}
