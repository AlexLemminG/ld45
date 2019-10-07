using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorGroup : MonoBehaviour {
    public int minCount = 1;
    public int maxCount = 1;
    public float minScale = 1;
    public float maxScale = 1;
    public float minPosRadius;
    public float posRadius;

    public float minSpeed = 0;
    public float maxSpeed = 2;
    public List<GameObject> prefabs = new List<GameObject> ();

    void Start () {
        float angle = Random.Range (0, 2 * Mathf.PI);
        float speed = Random.Range (minSpeed, maxSpeed);
        Vector2 dir = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle));
        transform.up = dir;
        int count = Random.Range (minCount, maxCount + 1);
        for (int i = 0; i < count; i++) {
            var prefab = prefabs[Random.Range (0, prefabs.Count)];
            float posAngle = Random.Range (0, 2 * Mathf.PI);
            var dirPos = new Vector2 (Mathf.Sin (posAngle), Mathf.Cos (posAngle));
            Vector3 position = dirPos * Random.Range (minPosRadius, posRadius);
            position = transform.TransformPoint (position);
            var rotation = Quaternion.Euler (0, 0, Random.Range (0, 360f));
            var scale = prefab.transform.localScale * Random.Range (minScale, maxScale);
            var obj = Instantiate (prefab, position, rotation);
            obj.transform.localScale = scale;
            bool isMeteor = obj.GetComponent<Meteor> () != null;
            if (isMeteor) {
                obj.GetComponent<Rigidbody2D> ().velocity = dir * speed;
            }
        }
    }

    void OnDrawGizmosSelected () {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (Vector3.zero, minPosRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (Vector3.zero, posRadius);
        Gizmos.matrix = Matrix4x4.identity;
    }
}