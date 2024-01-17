using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("References")]
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    [Header("Movement")]
    [SerializeField] private float speed = 140f;
    [SerializeField] private float turnSpeed = 200f;
    private Rigidbody2D rb;

    [Header("Shooting")]
    [SerializeField] private float bulletSpeed = 300f;

    [Header("Player")]
    [SerializeField] private int maxHealth = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = maxHealth;
    }

    private void Start()
    {
        OnCharacterDying += Player_OnCharacterDying;
    }

    private void Player_OnCharacterDying(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleRotation();
        if (playerInputManager.ShootInput()) Shoot();
    }

    private void FixedUpdate()
    {
        // Add force to forward direction
        rb.AddForce(playerInputManager.VerticalInput() * transform.up * speed * Time.fixedDeltaTime);
    }

    private void HandleRotation()
    {
        transform.Rotate(new Vector3(0, 0, -playerInputManager.HorizontalInput() * turnSpeed * Time.deltaTime));
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Destroy(bullet, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Asteroid asteroid))
        {
            TakeDamage(1); // Take 1 health
            asteroid.Explode();
            rb.AddForce((transform.position -  asteroid.transform.position) * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
}
