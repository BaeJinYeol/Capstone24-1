using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public float offset = 3.5f;

    public GameObject player;

    private void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        dir.y += offset;

        // 카메라 이동을 더 부드럽게 수정
        // transform.position = Vector3.Lerp(transform.position, dir, Time.deltaTime * cameraSpeed);

        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime,
                                         dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
