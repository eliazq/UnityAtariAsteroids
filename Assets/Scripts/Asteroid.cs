using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 200;
    private int health = 3;
    private int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (health <= 0) Destroy(gameObject);
            transform.localScale = transform.localScale / 1.3f;
        }
    }

    [SerializeField] private GameObject asteroidPrefab;
    private const string bulletTag = "Bullet";
    public static event EventHandler OnAsteroidExploding;

    private void Awake()  // When asteroid is spawn to the game
    {
        Vector2 targetPos = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)); // Asteroids target position to head, close to the middle of the screen

        Vector2 moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized; // Set moveDir for asteroids, add force to that direction

        GetComponent<Rigidbody2D>().AddForce(moveDir * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 14f); // Destroy asteroid after time
    }

    public void Explode()  // When asteroid is exploded, player has shot it
    {
        GameObject[] asteroids = { Instantiate(asteroidPrefab, transform.position, Quaternion.identity), Instantiate(asteroidPrefab, transform.position, Quaternion.identity) }; // Spawn 2 asteroids when this explodes
        foreach (GameObject asteroid in asteroids)
        {
            asteroid.GetComponent<Asteroid>().Health = Health - 1; // Give both asteroid smaller health than this, so there will be 2 smaller asteroids when this explodes
            asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * speed * Time.fixedDeltaTime, ForceMode2D.Impulse); // add them random movement
        }
        OnAsteroidExploding?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject); // Destroy this asteroid after spawning 2 smaller ones
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletTag))
        {
            Destroy(collision.gameObject); // destroy bullet
            Explode();
        }
    }
}
