using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour {
    void Start () {
        GetComponent<Rigidbody2D> ().mass *= transform.localScale.x;
    }
}