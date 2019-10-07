using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
    public List<GameObject> toSpawn;
    public float spawnRadius;
    public bool useScaleAsCountMultiplier = true;

    public void Die () {
        var deathParts = GetComponentsInChildren<DeathParts> (true);
        if (deathParts.Length > 0) {
            var parts = deathParts[0];
            parts.transform.SetParent (null);
            parts.transform.localScale = Vector3.one * transform.lossyScale.x;
            parts.gameObject.SetActive (true);
        }
        Destroy (gameObject);
        for (int i = 0; i < toSpawn.Count; i++) {
            var target = Random.Range (0, toSpawn.Count);
            var t = toSpawn[target];
            toSpawn[target] = toSpawn[i];
            toSpawn[i] = t;
        }

        int spawnCount = Mathf.Clamp ((int) (toSpawn.Count * (useScaleAsCountMultiplier ? transform.localScale.x : 1f)), Mathf.Min (1, toSpawn.Count), toSpawn.Count);
        for (int i = 0; i < spawnCount; i++) {
            float angle = Mathf.PI * 2 * i / spawnCount;
            var pos = new Vector2 (Mathf.Sin (angle), Mathf.Cos (angle)) * spawnRadius + (Vector2) transform.position;
            Instantiate (toSpawn[i], pos, Quaternion.Euler (0, 0, Random.Range (0f, 360f)));
        }
    }
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere (transform.position, spawnRadius);
    }
}