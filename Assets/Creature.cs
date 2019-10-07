using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {
    [Range (0, 1)]
    public float powerPercent;
    [Range (0, 1)]
    public float partsPercent;

    public float powerConsumptionMultiplier = 1f;

    public List<ActionGroup> actionGroups = new List<ActionGroup> ();
    void Awake () {
        Reinit ();
    }

    void Reinit () {
        CreateConnections ();
    }

    public void HandlePartsCountChanged () {
        if (Mathf.Abs (partsPercent - 1f) < 0.01f) {
            if (GetComponent<PlayerCreatureController> ()) {
                partsPercent = 0f;
                UpdatesPanel.panel.Show ();
            }
        }
    }
    public AudioSource powerupAudio;
    public AudioSource jointsAudio;
    public void HandleUpdateInstalled () {
        CreateConnections ();
        GetComponent<Health> ().currentAmount = GetComponent<Health> ().totalAmount;
        powerPercent = 1f;
        partsPercent = 0f;
        powerupAudio.Play ();
    }

    void Update () {
        if (Input.GetKeyDown (KeyCode.F1) && Input.GetKey (KeyCode.LeftControl)) {
            partsPercent = 1f;
            HandlePartsCountChanged ();
        }
        foreach (var motor in GetComponentsInChildren<Motor> ()) {
            motor.SetMovementDir (transform, movementDir.y, movementDir.x);
        }
    }
    void LateUpdate () {
        foreach (var c in GetComponentsInChildren<IPowerConsusumer> ()) {
            powerPercent -= c.GetConsumption () * Time.deltaTime * powerConsumptionMultiplier;
        }
        powerPercent = Mathf.Clamp01 (powerPercent);
    }

    public void SetActionGroupActive (int index, bool active) {
        if (index == 0 && jointsAudio != null) {
            jointsAudio.enabled = active;
        }
        if (actionGroups.Count > index) {
            actionGroups[index].SetActive (active);
        }
    }

    public Vector2 movementDir;

    public void CreateConnections () {
        actionGroups.Clear ();
        for (int i = 0; i < 2; i++) {
            actionGroups.Add (new ActionGroup ());
            actionGroups[actionGroups.Count - 1].key = KeyCode.Alpha1 + i;
        }
        actionGroups[0].key = KeyCode.Space;
        actionGroups[1].key = KeyCode.LeftControl;
        foreach (var joint in GetComponentsInChildren<IForcable> ()) {
            if (joint.actionGroup >= 0 && joint.actionGroup <= actionGroups.Count) {
                actionGroups[joint.actionGroup].actionTargets.Add (joint);
            }
        }
        foreach (var ag in actionGroups) {
            ag.SetActive (false);
        }
    }
}

public class ActionGroup {
    public KeyCode key;
    public List<IForcable> actionTargets = new List<IForcable> ();
    public bool active = true;
    public ActionGroup () {
        active = true;
        SetActive (false);
    }

    public void SetActive (bool active) {
        if (this.active == active) {
            return;
        }
        this.active = active;
        var jointsForce = active ? 1 : -1;
        foreach (var joint in actionTargets) {
            joint.SetForce (jointsForce);
        }

    }
}