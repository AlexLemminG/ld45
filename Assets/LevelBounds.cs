using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour {
    public Transform teleportTarget;

    void OnTriggerEnter2D (Collider2D collider) {
        var root = collider.transform.root;
        if (root.GetComponent<DestroyOnBounds> ()) {
            Destroy (root.gameObject);
            return;
        }
        var localPosition = transform.InverseTransformPoint (root.transform.position);
        localPosition.y = transform.InverseTransformPoint (teleportTarget.position).y;

        root.transform.position = transform.TransformPoint (localPosition);
    }
}