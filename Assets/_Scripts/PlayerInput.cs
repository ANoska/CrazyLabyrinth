using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public float speed = 0.0f;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement* speed * 2);
        if (this.gameObject.transform.position.y < -10) {
            Destroy(this.gameObject);
        }
    }
}
