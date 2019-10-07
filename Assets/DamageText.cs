using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour {
    public TextMesh tm;
    public void Set (int amount, bool isPlayer) {
        tm.text = (-amount).ToString ();
        tm.color = isPlayer ? Color.red : Color.white;
    }

    void Awake () {
        Destroy (gameObject, 1f);
    }
}