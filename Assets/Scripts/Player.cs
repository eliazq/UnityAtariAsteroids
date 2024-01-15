using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    [Header("References")]

    [Header("Movement")]
    [SerializeField] private float speed = 140f;
    [SerializeField] private float turnSpeed = 200f;
    private Rigidbody2D rb;
    float forward;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        forward = Input.GetAxisRaw("Vertical");
        if (forward < 0) forward = 0;

        HandleMovement(horizontal);
    }

    private void FixedUpdate()
    {
        // Add force to forward direction
        rb.AddForce(forward * transform.up * speed * Time.deltaTime);
    }

    private void HandleMovement(float horizontalInput)
    {
        transform.Rotate(new Vector3(0, 0, -horizontalInput * turnSpeed * Time.deltaTime));
    }
}
