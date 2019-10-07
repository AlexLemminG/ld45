using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IForcable, IPowerConsusumer {
    public Transform spawnPoint;
    public GameObject bulletPrefab;
    const float reloadTime = 0.5f;
    float lastTimeShot;
    float prevForce = -1f;

    public int m_actionGroup;
    public int actionGroup { get { return m_actionGroup; } }

    public void SetForce (float force) {
        if (force > prevForce) {
            if (Time.time - lastTimeShot > reloadTime) {
                ShootAtDir (spawnPoint.up);
            }
        }
        prevForce = force;
    }

    public void ShootAtDir (Vector2 dir) {
        if (Time.time - lastTimeShot <= reloadTime) {
            return;
        }
        var rotation = Quaternion.LookRotation (Vector3.back, dir);
        lastTimeShot = Time.time;
        var bullet = Instantiate (bulletPrefab, spawnPoint.position, rotation);
        bullet.GetComponent<Bullet> ().creator = GetComponentInParent<Creature> ();
    }

    public float powerPerShot = 0.1f;
    public float GetConsumption () {
        var shotThisFrame = Time.time == lastTimeShot;
        return (shotThisFrame ? powerPerShot : 0f) / Time.deltaTime;
    }
}
public interface IForcable {
    void SetForce (float force);
    int actionGroup { get; }
}