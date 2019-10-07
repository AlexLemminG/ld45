using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour {
    public float searchRadius;

    void Update () {
        var overlapping = Physics2D.OverlapCircleAll (transform.position, searchRadius);
        List<Creature> creatures = new List<Creature> ();
        foreach (var o in overlapping) {
            var c = o.GetComponentInParent<Creature> ();
            if (c == null || creatures.Contains (c)) {
                continue;
            }
            creatures.Add (c);
        }

        float closestDist = float.MaxValue;
        Creature closest = null;
        var thisCreature = GetComponentInParent<Creature> ();
        foreach (var c in creatures) {
            if (thisCreature == c) {
                continue;
            }
            var dir = transform.InverseTransformPoint (c.transform.position);
            if (dir.y < 0) {
                continue;
            }

            var dist = Vector3.Distance (transform.position, c.transform.position);
            if (dist < closestDist) {
                closestDist = dist;
                closest = c;
            }
        }

        if (closest != null) {
            GetComponent<Gun> ().ShootAtDir (closest.transform.position - transform.position);
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, searchRadius);
    }
}