using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConvertTest : MonoBehaviour
{
    [ContextMenu("正前方创造三个球")]
    public void Func()
    {
        for (int i = 1; i <= 3; ++i)
        {
            GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = this.transform.TransformPoint(Vector3.forward * i);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
