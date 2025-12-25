using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.mousePosition);
        #region 鼠标输入
        //if (Input.GetMouseButtonDown(0))
        //{
        //    print("鼠标左键被按下");
        //}
        //if (Input.GetMouseButtonUp(1))
        //{
        //    print("鼠标右键被抬起");
        //}
        //if (Input.GetMouseButton(2))
        //{
        //    print("鼠标中间正在按住");
        //}
        //if (Input.mouseScrollDelta == new Vector2(0, 1))
        //{
        //    print("鼠标滚轮向上滚动");
        //}
        //if (Input.mouseScrollDelta == new Vector2(0, -1))
        //{
        //    print("鼠标滚轮向下滚动");
        //}
        #endregion

        #region 键盘输入
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    print("前进");
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    print("停止前进");
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    print("一直前进");
        //}
        #endregion

        #region 检测默认轴输入
        ////Input.GetAxis和Input.GetAxisRaw方法
        //print($"水平移动{Input.GetAxis("Horizontal")}");
        //print($"垂直移动{Input.GetAxis("Vertical")}");
        //print($"鼠标X移动{Input.GetAxis("Mouse X")}");
        //print($"鼠标Y移动{Input.GetAxis("Mouse Y")}");
        #endregion

        #region 其他
        //是否有任意 键盘 或 鼠标 长按/按下
        if (Input.anyKey)
        {
            print($"正在长按按键：{Input.inputString}");
        }
        if (Input.anyKeyDown)
        {
            print($"按下了键:{Input.inputString}");
        }
        #endregion
    }
}
