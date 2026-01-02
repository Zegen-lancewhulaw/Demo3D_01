using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyTest : MonoBehaviour
{
    Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        // 1. 首先获取刚体组件
        rigidBody = GetComponent<Rigidbody>();
        // 2. 添加力
        // 相对世界坐标
        // rigidBody.AddForce(Vector3.forward * 100);
        // 相对本地坐标
        // rigidBody.AddRelativeForce(Vector3.forward * 100);

        // 3. 添加扭矩力
        // 相对世界坐标
        // rigidBody.AddTorque(Vector3.up * 100);
        // 相对本地坐标
        // rigidBody.AddRelativeTorque(Vector3.up * 100);

        // 4. 直接改变速度（相对世界坐标）
        // rigidBody.velocity = Vector3.forward * 2;

        // 5. 模拟爆炸效果
        rigidBody.AddExplosionForce(200, Vector3.zero, 10); // 爆炸力度200，爆炸中心(0,0,0)，爆炸半径10
    }

    // Update is called once per frame
    void Update()
    {

    }
}
