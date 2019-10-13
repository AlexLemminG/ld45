using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public int currentAmount;
    public int totalAmount = 100;
    void Awake () {
        currentAmount = totalAmount;
    }
    bool dead = false;
    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.gameObject.GetComponent<Bullet> () != null) {
            return;
        }
        var thisMass = GetComponent<Rigidbody2D> ().mass;
        float totalImpulse = 0;
        foreach (var c in collision.contacts) {
            totalImpulse += c.normalImpulse;
        }
        var damage = Mathf.FloorToInt (totalImpulse * 1f / thisMass);
        if (damage > 0) {
            DoDamage (damage, collision.contacts[0].point);
        }
    }

    public void DoDamage (int damage, Vector3 position) {
        if (damage <= 0) {
            return;
        }
        currentAmount -= damage;
        var damageTextPrefab = Resources.Load<DamageText> ("DamageText");
        Instantiate (damageTextPrefab, position, Quaternion.identity).Set (damage, GetComponentInParent<CameraTarget> () != null);
        if (currentAmount <= 0 && !dead) {
            dead = true;
            var deathHandler = GetComponent<DeathHandler> ();
            if (deathHandler) {
                deathHandler.Die ();
            }
        }
    }
}