using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed = 20f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        /// Movemos con las teclas 
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 moveVelocity = moveInput * speed;
        rb.linearVelocity = moveVelocity;
    }
};
