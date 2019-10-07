using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public List<GameObject> levelStages;

    public void StartNewStage (int index) {
        if (levelStages.Count > index && index >= 0) {
            levelStages[index].SetActive (true);
        }
    }
}