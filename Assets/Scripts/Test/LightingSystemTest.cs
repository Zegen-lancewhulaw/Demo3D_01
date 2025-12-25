using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingSystemTest : MonoBehaviour
{
    public Light lt;
    public float moveSpeed = 10f;
    public float distance = 10f;
    private float rollAngle = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if(lt != null)
        {
            lt.transform.position = new Vector3(distance, 0, 0);
            lt.transform.LookAt(Vector3.zero);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lt != null)
        {
            // 计算光源位置
            rollAngle += moveSpeed * Time.deltaTime;
            if (rollAngle >= 360)
                rollAngle -= 360;
            Quaternion rollAmount = Quaternion.Euler(0, 0, rollAngle);
            Vector3 disAngle = rollAmount * Vector3.right;
            Vector3 disPosition = disAngle * distance;
            lt.transform.position = disPosition;
            lt.transform.LookAt(Vector3.zero);
        }
    }
}
