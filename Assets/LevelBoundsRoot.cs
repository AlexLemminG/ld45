using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundsRoot : MonoBehaviour {
    Transform source;

    void Start () {
        source = GameObject.FindGameObjectWithTag ("CameraRig").transform;
    }

    void Update () {
        if (source == null) {
            return;
        }
        transform.position = source.position;
    }
}