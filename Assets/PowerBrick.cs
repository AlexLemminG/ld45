using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBrick : MonoBehaviour {
    const float powerPercentPerBrick = 0.25f;
    void OnTriggerEnter2D (Collider2D collider) {
        var creature = collider.GetComponentInParent<Creature> ();
        if (creature != null && this.enabled && !collider.GetComponentInParent<PowerUpMagnet> ()) {
            this.enabled = false;
            creature.powerPercent += powerPercentPerBrick;
            var audio = creature.GetComponentInChildren<AudioSource> ();
            if (audio != null) {
                audio.Play ();
            }
            Destroy (gameObject);
        }
    }
}