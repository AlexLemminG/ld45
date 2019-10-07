using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolloweCamera : MonoBehaviour {
    public float lerpPower;
    Transform target;
    float startSize;
    Camera cam;
    void Start () {
        target = FindObjectOfType<CameraTarget> ().transform;
        prevPos = target.position;
        cam = GetComponentInChildren<Camera> ();
        startSize = cam.orthographicSize;
    }

    Vector3 prevPos;
    Vector3 speed;
    void Update () {
        speed = Vector3.Lerp (speed, (target.position - prevPos) / Time.deltaTime, Time.deltaTime * 5f);
        prevPos = target.position;
        transform.position = Vector3.Lerp (transform.position, target.position + speed, lerpPower * Time.deltaTime);
        cam.orthographicSize = startSize * Mathf.Lerp (1f, 1.2f, speed.magnitude / 5);
    }
}