using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerBattery : MonoBehaviour {
    float powerConsumptionMultiplier = 0.2f;
    void Awake () {
        GetComponentInParent<Creature> ().powerConsumptionMultiplier = powerConsumptionMultiplier;
    }
}