using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsCreator : MonoBehaviour {
    public int count;
    public float radius;
    public List<GameObject> prefabs;

    public List<Transform> layers;
    Vector3 originalPosition;
    Transform cameraRig;
    void Start () {
        originalPosition = transform.position;
        cameraRig = GameObject.FindGameObjectWithTag ("CameraRig").transform;
        float scale = 1f;
        foreach (var l in layers) {
            scale *= 0.92f;
            var layerScale = l.transform.lossyScale.x;
            for (int i = 0; i < count / Mathf.Pow (scale, 10); i++) {
                var pos = new Vector2 (Random.Range (-radius, radius), Random.Range (-radius, radius));
                pos = l.TransformPoint (pos);
                var star = Instantiate (prefabs[Random.Range (0, prefabs.Count)], pos, Quaternion.Euler (0, 0, Random.Range (0, 360)), l);
                star.transform.localScale = Vector3.one * scale / layerScale;
            }
        }
    }

    void LateUpdate () {
        float scale = 1f;
        foreach (var l in layers) {
            scale *= 0.95f;
            l.position = Vector3.Lerp (originalPosition, cameraRig.position, scale);
        }
    }
}