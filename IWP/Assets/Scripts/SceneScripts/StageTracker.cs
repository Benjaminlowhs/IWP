using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using Cinemachine;

public class StageTracker : MonoBehaviour
{


    public float timeToShake = 10f;

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    private float timer = 0f;
    private float shakeIntensity = 2;

    public GameObject player;
    public PlayerStats playerStats;

    float shakeTime = 0.2f;

    private CinemachineBasicMultiChannelPerlin _cbmcp;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerStats.stage == 2)
        {
            timeToShake -= Time.deltaTime;
            if (timeToShake < 0f)
            {
                timeToShake = 10f;
                // Play camera shake
                ShakeCamera();
                //Debug.Log("Shaking");

            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                }
            }
        }
    }

    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cbmcp = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = shakeIntensity;
        timer = 2f;
    }
}
