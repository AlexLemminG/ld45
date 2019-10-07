using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour, IPowerConsusumer, IForcable {

    public int m_actionGroup;
    public int actionGroup { get { return m_actionGroup; } }

    Rigidbody2D bodyA;
    Rigidbody2D bodyB;
    LineRenderer lr;
    public float motorSpeed = 5;
    public float motorForce = 500;
    public bool autoDirection = true;
    public enum Type {
        fixedJ,
        hinge
    }
    public Type type;
    public float deltaAngle = 10f;
    Joint2D joint;
    void Start () {
        bodyB = transform.parent.GetComponent<Rigidbody2D> ();
        transform.SetParent (transform.parent.parent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        bodyA = GetComponent<Rigidbody2D> ();
        GetComponent<FixedJoint2D> ().connectedBody = transform.parent.gameObject.GetComponent<Rigidbody2D> ();

        if (autoDirection) {
            bool inverse = GetComponentInParent<Creature> ().transform.InverseTransformPoint (bodyB.transform.position).x < 0;
            if (inverse) {
                motorSpeed = -motorSpeed;
            }
        }

        lr = GetComponent<LineRenderer> ();
        JointMotor2D motor = new JointMotor2D ();
        motor.motorSpeed = motorSpeed;
        motor.maxMotorTorque = motorForce;

        var dir = bodyA.transform.InverseTransformPoint (bodyB.position);
        var angle = -Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg * 0;
        var angleLimits = new JointAngleLimits2D ();
        angleLimits.min = angle - deltaAngle;
        angleLimits.max = angle + deltaAngle;

        switch (type) {
            case Type.fixedJ:
                joint = bodyA.gameObject.AddComponent<FixedJoint2D> ();
                break;
            case Type.hinge:
                var j = bodyA.gameObject.AddComponent<HingeJoint2D> ();
                j.limits = angleLimits;
                j.useLimits = true;
                j.motor = motor;
                joint = j;
                break;
        }

        joint.connectedBody = bodyB;

        SetForce (-1);
    }
    void Update () {
        lr.SetPosition (0, bodyA.position);
        lr.SetPosition (1, bodyB.position);
    }

    void OnDestroy () {
        if (joint) {
            Destroy (joint);
        }
    }

    float force;
    public float powerPerForce = 0.02f;
    public void SetForce (float force) {
        this.force = force;
        var hinge = joint as HingeJoint2D;
        if (hinge != null) {
            var motor = hinge.motor;
            motor.motorSpeed = force * motorSpeed;
            hinge.motor = motor;
            hinge.limits = hinge.limits;
            hinge.useLimits = false;
            hinge.useLimits = true;
            GetComponent<Rigidbody2D> ().AddForce (Vector2.zero);
            hinge.connectedBody.AddForce (Vector2.zero);
            var color = Color.Lerp (Color.blue, Color.red, (force + 1) / 2f);
            lr.endColor = color;
        }
    }

    public float GetConsumption () {
        return Mathf.Max (force * powerPerForce, 0);
    }

    void OnDrawGizmos () {
        Gizmos.color = Color.black;
        if (transform.parent && transform.parent.parent) {
            Gizmos.DrawLine (transform.parent.position, transform.parent.parent.position);
        }
    }
}