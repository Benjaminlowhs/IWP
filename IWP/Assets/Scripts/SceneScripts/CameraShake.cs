using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{
    float timer = 3f;
    CameraShake cameraShaker;
    bool isShaking = false;
    float shakeTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        cameraShaker = gameObject.GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
        //if (timer < 0)
        //{
        //    cameraShaker.enabled = true;
        //    CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        //    isShaking = true;
        //}
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        //if (isShaking)
        //{
        //    shakeTimer -= Time.deltaTime;
        //    if (shakeTimer < 0)
        //    {
        //        cameraShaker.enabled = false;
        //        shakeTimer = 2f;
        //        timer = 3f;
        //    }
        //}
    }
}
