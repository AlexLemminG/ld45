using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWhenActive : MonoBehaviour {
    public GameObject toActivate;
    void Start () {
        toActivate.SetActive (true);
    }
}