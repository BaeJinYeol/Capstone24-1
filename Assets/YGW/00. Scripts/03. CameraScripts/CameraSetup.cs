using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSetup : MonoBehaviour
{
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera virtualCam;

    private void Awake()
    {
        virtualCam = GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    private void OnEnable()
    {
        GameObject obj = GameObject.Find("RoguePlayer");
        virtualCam.Follow = obj.GetComponentsInChildren<Transform>()[1];
    }
}
