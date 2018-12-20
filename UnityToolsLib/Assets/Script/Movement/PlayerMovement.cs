using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float playerSpeed;
    public float dashForce;

    private Rigidbody2D _rig2D;

    private void Awake()
    {
        _rig2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate () {
        Vector2 _direction = new Vector2();

        if (Input.GetKey(KeyCode.Q)) {
            _direction.x = -transform.right.x;
        } else if (Input.GetKey(KeyCode.D)) {
            _direction.x = transform.right.x;
        }

        if (Input.GetKey(KeyCode.Z)) {
            _direction.y = transform.up.y;
        } else if (Input.GetKey(KeyCode.S)) {
            _direction.y = -transform.up.y;
        }

        _rig2D.AddForce(_direction.normalized * playerSpeed, ForceMode2D.Force);

        // Dashing
        if (Input.GetKeyDown(KeyCode.E)) {
            _rig2D.AddForce(_direction.normalized * dashForce, ForceMode2D.Impulse);
        }
    }
}
