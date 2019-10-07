using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemiesLeftText : MonoBehaviour {
    public TMP_Text text;
    public GameObject noEnemiesLeftScreen;
    void Update () {
        var creaturesCount = FindObjectsOfType<Creature> ().Where (x => x.GetComponent<PlayerCreatureController> () == null).Count ();
        text.text = "enemies left: " + (creaturesCount).ToString ();
        if (creaturesCount == 0) {
            noEnemiesLeftScreen.SetActive (true);
        }
    }
}