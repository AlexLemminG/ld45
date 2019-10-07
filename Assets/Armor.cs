using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour {
    public float healthMultiplier = 2f;

    void Awake () {
        GetComponentInParent<Health> ().totalAmount = Mathf.RoundToInt (GetComponentInParent<Health> ().totalAmount * healthMultiplier);
    }
}