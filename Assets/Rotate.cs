using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    public float anglePerSecond = 90;

    // Update is called once per frame
    void Update () {
        var euler = transform.localEulerAngles;
        euler.z += Time.deltaTime * anglePerSecond;
        transform.localEulerAngles = euler;
    }
}