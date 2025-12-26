using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FireControlSystem : MonoBehaviour
{
    // ==================================================
    // 1. 配置区域
    // ==================================================

    [Header("Configurations")]
    [Tooltip("弹夹容量")]
    [SerializeField] int magazineVolumn = 6;
    [Tooltip("发射冷却时间（秒）")]
    [SerializeField] float fireInterval = 0.5f;
    [Tooltip("弹药补充时间（秒）")]
    [SerializeField] float replenishingLifeTime = 4f;

    [Tooltip("弹道随机散布范围（度）")]
    [SerializeField] private float spreadAngle = 0.5f;
    [Tooltip("炮口生成位置的前向偏移量")]
    [SerializeField] private float muzzleOffset = 4.5f;

    // ==================================================
    // 2. 引用区域
    // ==================================================

    [Header("References")]
    [Tooltip("弹药预制体引用")]
    [SerializeField] private GameObject shellPrefab;// 引用炮弹预制体

    private Queue<GameObject> _magazine = new Queue<GameObject>();   // 弹匣
    private List<GameObject> _shellPools = new List<GameObject>();   // 用于防止对象池中的炮弹GC，仅持有引用

    // ==================================================
    // 3. 状态区域
    // ==================================================

    private float _lastFireTime; // 上次发射时间

    // ==================================================
    // 4. 生命周期
    // ==================================================

    private void Awake()
    {
        Initialize();
    }

    void Update()
    {
        // 检测到按下鼠标左键 并且 此时距离上次开火时间已经超过发射冷却时间 则尝试开火
        if (Input.GetMouseButtonDown(0) && (Time.time - _lastFireTime) > fireInterval)
        {
            TryFire();
        }
    }

    // ==================================================
    // 5. 私有逻辑方法
    // ==================================================

    /// <summary>
    /// 初始化火控系统，创建magazineVolume个Shell预制体的对象，并加入_magazine队列
    /// 设置上次开火时间_lastFireTime设置为 -fireInterval以保证马上就能开火
    /// </summary>
    void Initialize()
    {
        if (shellPrefab != null)
        {
            for (int i = 0; i < magazineVolumn; i++)
            {
                GameObject shell = GameObject.Instantiate(shellPrefab);
                shell.name = $"shell_{i}";
                shell.SetActive(false);

                _magazine.Enqueue(shell);
                _shellPools.Add(shell);
            }
        }
        _lastFireTime = -fireInterval;
    }

    /// <summary>
    /// 尝试开火
    /// </summary>
    void TryFire()
    {
        GameObject shell;
        if((shell = _magazine.Dequeue()) != null){
            SetSpawnPosition(shell);
            SetSpawnRotation(shell);
            shell.SetActive(true);
            shell.GetComponent<ShellMove>().Init((shell) =>
            {
                shell.SetActive(false);
                StartCoroutine(ReplenishRoutine(shell));
            });
            _lastFireTime = Time.time;
        }
    }

    /// <summary>
    /// 设置炮弹发射时的初始位置
    /// </summary>
    /// <returns></returns>
    void SetSpawnPosition(GameObject shell)
    {
        shell.transform.position = this.transform.TransformPoint(Vector3.forward * muzzleOffset);
    }

    /// <summary>
    /// 设置炮弹发射时的初始角度
    /// </summary>
    /// <returns></returns>
    void SetSpawnRotation(GameObject shell)
    {
        // 计算后坐力导致的发射角度随机扰动
        Quaternion disturbance = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0
            );
        shell.transform.rotation = this.transform.rotation * disturbance;
    }

    /// <summary>
    /// 等待直至满replenishingLifeTime秒的时间后，这颗弹药重新填入magazzine，以备射击
    /// </summary>
    /// <param name="shell">生命周期终止后失活、进入弹药补充期的一颗炮弹</param>
    /// <returns></returns>
    IEnumerator ReplenishRoutine(GameObject shell)
    {
        yield return new WaitForSeconds(replenishingLifeTime);

        _magazine.Enqueue(shell);
    }
}