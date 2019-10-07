using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLevelPanel : MonoBehaviour {
    public GameObject locker;

    public void Update () {
        var prevPanelIndex = transform.GetSiblingIndex () - 1;

        UpdateLevelPanel prevPanel = null;
        if (prevPanelIndex >= 0) {
            prevPanel = transform.parent.GetChild (prevPanelIndex).GetComponent<UpdateLevelPanel> ();
        }
        if (prevPanel) {
            bool allEnabled = true;
            foreach (var button in prevPanel.GetComponentsInChildren<Button> ()) {
                if (button.interactable) {
                    allEnabled = false;
                }
            }
            bool lockerWasActive = locker.activeSelf;
            locker.SetActive (!allEnabled);
            if (lockerWasActive && !locker.activeSelf) {
                FindObjectOfType<LevelManager> ().StartNewStage (transform.GetSiblingIndex ());
            }
        } else {
            locker.SetActive (false);

        }

    }
}