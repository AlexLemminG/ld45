using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatesPanel : MonoBehaviour {
    public static UpdatesPanel panel;

    float originalFixedDelta;
    const float timeScale = 0.05f;
    void Awake () {
        panel = this;
        originalFixedDelta = Time.fixedDeltaTime;
    }
    void Start () {
        gameObject.SetActive (false);
    }
    void OnEnable () {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = originalFixedDelta * timeScale;
    }
    void OnDisable () {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDelta;
    }

    public void Show () {
        bool hasInterable = false;
        foreach (var b in GetComponentsInChildren<UpdateCreatureButton> ()) {
            if (b.GetComponentInChildren<Button> ().interactable) {
                hasInterable = true;
                break;
            }
        }
        if (hasInterable) {
            gameObject.SetActive (true);
        }
    }

    public void Hide () {
        foreach (var b in GetComponentsInChildren<UpdateCreatureButton> ()) {
            b.Update ();
        }
        foreach (var p in GetComponentsInChildren<UpdateLevelPanel> ()) {
            p.Update ();
        }
        gameObject.SetActive (false);
    }
}