using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsBrick : MonoBehaviour {
    public static float partsPercentPerBrick = 0.25f;
    void OnTriggerEnter2D (Collider2D collider) {
        var creature = collider.GetComponentInParent<Creature> ();
        if (creature != null && this.enabled && !collider.GetComponentInParent<PowerUpMagnet> ()) {
            this.enabled = false;
            creature.partsPercent += partsPercentPerBrick;
            creature.HandlePartsCountChanged ();
            var audio = creature.GetComponentInChildren<AudioSource> ();
            if (audio != null) {
                audio.Play ();
            }
            Destroy (gameObject);
        }
    }
}