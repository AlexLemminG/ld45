using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCreatureButton : MonoBehaviour {
    public string updateName;

    public Color activeColor = Color.white;
    public Color purchasedColor = Color.green;
    Button button;
    Creature playerCreature;
    GameObject update;
    ColorBlock colorsOrig;

    void Start () {
        button = GetComponent<Button> ();
        button.GetComponentInChildren<TMP_Text> ().text = updateName;
        playerCreature = FindObjectOfType<PlayerCreatureController> ().GetComponent<Creature> ();
        update = playerCreature.transform.Find (updateName)?.gameObject;
        button.onClick.AddListener (PurchaseUpdate);
        colorsOrig = button.colors;
    }

    void PurchaseUpdate () {
        update.SetActive (true);
        update.GetComponentInParent<Creature> ().HandleUpdateInstalled ();
        GetComponentInParent<UpdatesPanel> ().Hide ();
    }

    public void Update () {
        button.interactable = update != null && !update.activeSelf;
        var color = button.interactable ? activeColor : purchasedColor;
        var colors = colorsOrig;
        colors.normalColor *= color;
        colors.disabledColor *= color;
        colors.highlightedColor *= color;
        colors.pressedColor *= color;
        button.colors = colors;
    }
}