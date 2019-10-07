using UnityEngine;

public class Bullet : MonoBehaviour {
    public float speed = 20f;
    public int damageAmount = 1;

    [HideInInspector]
    public Creature creator;
    void Start () {
        GetComponent<Rigidbody2D> ().velocity = (Vector2) transform.up * speed;
    }

    void OnTriggerEnter2D (Collider2D c) {
        if (!enabled) {
            return;
        }
        var deflector = c.gameObject.GetComponentInParent<DeflectorShield> ();
        if (deflector && deflector.GetComponentInParent<Creature> () != creator) {
            deflector.Damaged ();
            enabled = false;
            Destroy (gameObject);
        }
    }

    void OnCollisionEnter2D (Collision2D c) {
        if (enabled) {
            enabled = false;
            Destroy (gameObject);
            var health = c.gameObject.GetComponentInParent<Health> ();
            if (health) {
                var deflector = c.gameObject.GetComponentInParent<DeflectorShield> ();
                if (deflector != null) {
                    deflector.Damaged ();
                } else {
                    health.DoDamage (damageAmount, transform.position);
                }
            }
        }
    }
}