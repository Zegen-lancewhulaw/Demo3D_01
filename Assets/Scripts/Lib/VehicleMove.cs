using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// 控制载具的移动、炮塔旋转以及开火逻辑
/// </summary>
public class VehicleMove : MonoBehaviour
{
    // =================================================
    // 1. 配置区域 (Configuration) - 使用 Header 分组，Inspector 可见
    // =================================================

    [Header("Movement Settings")]
    [Tooltip("车身移动速速")]
    [SerializeField] private float moveSpeed = 5f;
    [Tooltip("车身转向速度")]
    [SerializeField] private float rotateSpeed = 90f;

    [Header("Turret Settings")]
    [Tooltip("炮塔转向速度（度）")]
    [SerializeField] private float turretRotateSpeed = 540f;
    [Tooltip("炮管抬降速度（度）")]
    [SerializeField] private float barrelRotateSpeed = 2700f;


    // =================================================
    // 2. 引用区域 (References) - 缓存组件引用
    // =================================================

    private Transform _turret;
    private Transform _barrel;

    // =================================================
    // 3. 状态区域 (State) - 运行时变化的变量
    // =================================================

    private float _barrelAngle = 0; //记录当前炮管角度，范围0~-60度

    // =================================================
    // 4. 生命周期 (Unity Lifecycle)
    // =================================================

    void Start()
    {
        InitializeReferences();
    }

    void Update()
    {
        HandleMovement();

        if (_turret != null)
            HandleTurretRotation();

        if (_barrel != null)
        {
            HandleBarrelRotation();
        }
    }

    // =================================================
    // 5. 私有逻辑方法 (Private Logic Methods)
    // =================================================

    /// <summary>
    /// 初始化组件引用，并进行防御性检查
    /// </summary>
    private void InitializeReferences()
    {
        _turret = this.transform.Find("Turret");
        _barrel = this.transform.DeepFind("Barrel");//注意：DeepFind是拓展方法，请确保项目中包含该扩展类

        if (_turret == null) Debug.LogWarning($"{name} : 未找到 Turret 子物体！");
        if (_barrel == null) Debug.LogWarning($"{name} : 未找到 Barrel 子物体！");
    }

    /// <summary>
    /// 处理车身的前后移动和左右转向
    /// </summary>
    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); //旋转速度校正系数
        float v = Input.GetAxis("Vertical"); //移动速度校正系数

        //旋转
        if(Mathf.Abs(h) > 0.01f)
        {
            float rotationAmount = h * rotateSpeed * Time.deltaTime;
            this.transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
        //移动
        if(Mathf.Abs(v) > 0.01f)
        {
            Vector3 movement = Vector3.forward * v * moveSpeed * Time.deltaTime;
            this.transform.Translate(movement, Space.Self);
        }
    }
    
    /// <summary>
    /// 处理炮塔跟随鼠标旋转
    /// </summary>
    private void HandleTurretRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");// 炮塔转向速度校正系数
        float rotationAmount = mouseX * turretRotateSpeed * Time.deltaTime;
        _turret.Rotate(Vector3.up, rotationAmount, Space.Self);
    }

    /// <summary>
    /// 处理炮管跟随鼠标滚轮抬起或降下
    /// </summary>
    private void HandleBarrelRotation()
    {
        float mouseS = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mouseS) < 0.001f) return;
        float rotationAmount = mouseS * barrelRotateSpeed * Time.deltaTime;
        _barrelAngle += rotationAmount;
        _barrelAngle = Mathf.Clamp(_barrelAngle, -60f, 0f);
        _barrel.localEulerAngles = new Vector3(_barrelAngle, 0, 0);
    }
}
