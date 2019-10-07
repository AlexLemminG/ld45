using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorShield : MonoBehaviour {
    public GameObject shield;
    public float restoreTime = 1f;

    void Restore () {
        shield.GetComponent<Animator> ().SetBool ("active", true);
        // shield.SetActive (true);
    }
    public void Damaged () {
        shield.GetComponent<Animator> ().SetBool ("active", false);
        // shield.SetActive (false);
        Invoke ("Restore", restoreTime);
    }
}