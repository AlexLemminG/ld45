using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour, IPowerConsusumer {
    public float force = 5f;
    public float powerPercentPerSecond = 0.01f;
    public ParticleSystem particles;

    public AudioSource audioSource;
    float consumption;
    Vector3 lookDirImmediate;
    float particlesRate;
    float audioVolume;
    void Awake () {
        particlesRate = particles.emission.rateOverTimeMultiplier;
        lookDir = Vector3.up;
        if (audioSource != null) {
            audioVolume = audioSource.volume;
            audioSource.enabled = true;
        }
    }

    public void SetMovementDir (Transform parent, float forwardPower, float rotationPower) {
        var forceA = parent.up * forwardPower * force;
        GetComponent<Rigidbody2D> ().AddForce (forceA, ForceMode2D.Force);

        var dirToParent = parent.InverseTransformPoint (transform.position).normalized;
        var dirToRotation = new Vector2 (-dirToParent.y, dirToParent.x) * rotationPower;
        var forceB = transform.TransformDirection (dirToRotation * force);
        GetComponent<Rigidbody2D> ().AddForce (forceB, ForceMode2D.Force);

        consumption = new Vector2 (forwardPower, rotationPower).magnitude * powerPercentPerSecond;

        if ((forceA + forceB).sqrMagnitude < Mathf.Epsilon) {
            lookDirImmediate = transform.InverseTransformDirection (parent.up);
        } else {
            lookDirImmediate = transform.InverseTransformDirection (forceA + forceB);
        }
    }
    Vector3 lookDir;
    void Update () {
        lookDir = Vector3.Lerp (lookDir, lookDirImmediate, Time.deltaTime * 3f);
        particles.transform.localRotation = Quaternion.LookRotation (-lookDir);
        var emissionRate = particles.emission;
        emissionRate.rateOverTimeMultiplier = Mathf.Clamp01 (consumption / powerPercentPerSecond) * particlesRate;

        if (audioSource != null) {
            audioSource.volume = consumption / powerPercentPerSecond * audioVolume;
        }
        // particles.emissionRate = emissionRate;
    }

    public float GetConsumption () {
        return consumption;
    }
}