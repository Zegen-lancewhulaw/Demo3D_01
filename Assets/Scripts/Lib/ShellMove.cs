using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellMove : MonoBehaviour
{
    public float speed = 15f;
    //float lifeTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(this.gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}
