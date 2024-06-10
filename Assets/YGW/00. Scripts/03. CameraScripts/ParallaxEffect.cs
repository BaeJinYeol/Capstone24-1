using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPos;
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPos;
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget) > 0 ?
    //    cam.farClipPlane : cam.nearClipPlane);

    float clippingPlane = 0.1f;

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.localPosition.z;
    }

    void Update()
    {
        Vector2 newPos = startingPos + camMoveSinceStart / parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}
