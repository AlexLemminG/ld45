using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionGroupTrigger : MonoBehaviour {
    public int groupIndex;
    float timeTriggered;
    float time;
    Creature thisCreature;

    void Start () {
        thisCreature = GetComponentInParent<Creature> ();
    }

    void OnTriggerStay2D (Collider2D collider) {
        var c = collider.GetComponentInParent<Creature> ();
        if (c != null && c != thisCreature) {
            timeTriggered = Time.time;
        }
    }

    void Update () {
        float period = 1f;
        if (Time.time - timeTriggered < 0.5f) {
            time += Time.deltaTime;
            thisCreature.SetActionGroupActive (groupIndex, Mathf.Repeat (time, period) > period / 2f);
        } else {
            thisCreature.SetActionGroupActive (groupIndex, false);
        }
    }
}