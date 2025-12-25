using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(Screen.currentResolution);
        Resolution r = Screen.currentResolution;
        print($"显示器分辨率，宽：{r.width}，高：{r.height}");
        print($"窗口分辨率，宽：{Screen.width}，高：{Screen.height}");
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Screen.fullScreen = true;
    }

    // Update is called once per frame
    void Update()
    {
        print(Input.GetAxis("Mouse ScrollWheel"));
    }
}
