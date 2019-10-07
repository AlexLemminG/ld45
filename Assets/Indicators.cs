using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour {
    public Image power;
    public Image parts;
    public Image health;
    Creature creature;
    Health healthComp;
    void Awake () {
        creature = GetComponentInParent<Creature> ();
        healthComp = creature.GetComponent<Health> ();
    }
    void Update () {
        power.fillAmount = creature.powerPercent;
        parts.fillAmount = creature.partsPercent;
        health.fillAmount = healthComp.currentAmount * 1f / healthComp.totalAmount;
    }
}