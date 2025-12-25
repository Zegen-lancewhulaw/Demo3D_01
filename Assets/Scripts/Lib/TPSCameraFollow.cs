using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCameraFollow : MonoBehaviour
{
    // =============================================
    // 1. 配置区域
    // =============================================

    [Header("Settings")]
    public float distance = 10.0f; // 距离目标的距离
    public float height = 5.0f; // 距离目标的高度
    public float rotateSpeed = 270f; // 右键旋转速度（度）
    public float smoothSpeed = 10f; // 跟随平滑度（阻尼）

    // =============================================
    // 2. 引用区域
    // =============================================

    [Header("Target")]
    [Tooltip("要跟随的目标")]
    public Transform target;

    // =============================================
    // 3. 状态区域
    // =============================================

    [Header("States")]
    // 内部变量：记录当前相机绕目标的角度
    private float _currentYaw = 0f; // 偏航角
    //private float _currentPitch = 0f; // 俯仰角（可选）
    //private float _currentRoll = 0f; // 翻滚角（可选）

    // =============================================
    // 4. 生命周期
    // =============================================

    void Start()
    {
        if(target != null)
        {
            _currentYaw = target.eulerAngles.y;
        }
    }


    void LateUpdate()
    {
        if (target == null) return;
        HandleInput();
        MoveCamera();
    }

    // =============================================
    // 5. 私有逻辑方法
    // =============================================

    private void HandleInput()
    {
        if (Input.GetMouseButton(1))
        {
            _currentYaw += Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        }
        else
        {
            _currentYaw = Mathf.LerpAngle(_currentYaw, target.transform.eulerAngles.y, Time.deltaTime * 2f);
        }
    }

    private void MoveCamera()
    {
        //计算相机目标位置
        Quaternion rotation = Quaternion.Euler(0, _currentYaw, 0);
        Vector3 direction = rotation * Vector3.back;
        Vector3 distinationPosition = target.position + Vector3.up * height + direction * distance;
        //缓慢移动相机（相机当前位置和目标位置之间插值）
        this.transform.position = Vector3.Lerp(this.transform.position, distinationPosition, smoothSpeed * Time.deltaTime);
        //相机看向目标头上的某个位置
        this.transform.LookAt(target.position + height * 0.5f * Vector3.up);
    }
}
