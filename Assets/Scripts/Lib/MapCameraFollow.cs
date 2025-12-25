using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    // ============================================================
    // 1. 配置区域
    // ============================================================

    [Header("Settings")]
    [SerializeField] private float height = 50f; // 地图摄像机距离目标的高度

    // ============================================================
    // 2. 引用区域
    // ============================================================

    [Header("References")]
    public Transform target;

    // ============================================================
    // 3. 状态区域
    // ============================================================

    // ============================================================
    // 4. 生命周期
    // ============================================================

    void Start()
    {
        InitializeCamera();
    }


    void LateUpdate()
    {
        if(this.target != null)
        {
            UpdateCamera();
        }
    }

    // ============================================================
    // 5. 私有逻辑方法
    // ============================================================

    private void InitializeCamera()
    {
        this.transform.eulerAngles = new Vector3(90, 0, 0);
        if (target != null)
            this.transform.position = target.position + Vector3.up * height;
        else
            this.transform.position = Vector3.up * height;
    }

    private void UpdateCamera()
    {
        this.transform.position = target.position + Vector3.up * height;
    }
}
