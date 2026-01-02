using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            audioSource.Play();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            audioSource.Stop();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Pause();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            audioSource.UnPause(); // 暂停后用Play()和UnPause()效果是一样的
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            audioSource.PlayDelayed(5); // 延迟5秒后播放
        }

        // 检测是否正在播放
        if (!audioSource.isPlaying)
        {
            print("没在播放");
        }
    }
}
