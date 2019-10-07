using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStage : MonoBehaviour {
    public int partsPerUpdate;
    public List<GameObject> activateOnStart;
    public List<GameObject> deactivateOnStart;
    void Start () {
        PartsBrick.partsPercentPerBrick = 1f / partsPerUpdate;
        foreach (var go in activateOnStart) {
            go.SetActive (true);
        }
        foreach (var go in deactivateOnStart) {
            go.SetActive (false);
        }
    }
}