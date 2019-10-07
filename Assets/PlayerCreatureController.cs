using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreatureController : MonoBehaviour {
    public GameObject noHealthScreen;
    public GameObject noPowerScreen;
    Creature creature;
    void Start () {
        creature = GetComponent<Creature> ();
    }

    // Update is called once per frame
    void Update () {
        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");
        creature.movementDir = Vector2.ClampMagnitude (new Vector2 (h, v), 1f);
        for (int i = 0; i < creature.actionGroups.Count; i++) {
            ActionGroup actionGroup = (ActionGroup) creature.actionGroups[i];
            creature.SetActionGroupActive (i, Input.GetKey (actionGroup.key));
        }
        if (creature.powerPercent <= 0f) {
            noPowerScreen.SetActive (true);
            Disable ();
        }
        if (creature.GetComponent<Health> ().currentAmount <= 0) {
            noHealthScreen.SetActive (true);
            Disable ();
        }
    }

    void Disable () {
        creature.movementDir = Vector2.zero;
        for (int i = 0; i < creature.actionGroups.Count; i++) {
            ActionGroup actionGroup = (ActionGroup) creature.actionGroups[i];
            creature.SetActionGroupActive (i, false);
        }
    }
}