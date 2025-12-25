using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在到摄像机上，让摄像机跟随一个GameObject移动并一直看向该GameObject
public class CameraFollow : MonoBehaviour
{
    public Transform target;//摄像机跟随并看向的物体的位置
    public float speed;//摄像机的跟随速度
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            this.transform.LookAt(target);
            if (Vector3.Distance(this.transform.position, target.transform.position) >= 20)
            {
                this.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }
        }
    }
}
