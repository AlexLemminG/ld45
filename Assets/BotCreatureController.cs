using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCreatureController : MonoBehaviour {
    Creature creature;
    void Start () {
        creature = GetComponent<Creature> ();
    }

    void Update () {
        creature.movementDir = Vector2.ClampMagnitude (new Vector2 (0, 1), 1f);
    }
}