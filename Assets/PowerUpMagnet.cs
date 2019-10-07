using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : MonoBehaviour {
    void OnTriggerStay2D (Collider2D c) {
        if (c.GetComponentInParent<PowerBrick> () || c.GetComponentInParent<PartsBrick> ()) {
            var dir = (Vector2) (transform.position - c.transform.position);
            c.GetComponent<Rigidbody2D> ().AddForce (dir.normalized * (1f / Mathf.Max (dir.sqrMagnitude, 0.1f)));
        }
    }
}