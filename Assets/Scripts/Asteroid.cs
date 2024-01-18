using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 200;
    private int health;
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

    private void Awake()
    {
        Health = 3;
        Vector2 targetPos = new Vector2(Random.Range(-9, 9), Random.Range(-4, 4)); // Asteroids target position to head, close to the middle of the screen
        Vector2 moveDir = (targetPos - new Vector2(transform.position.x, transform.position.y)).normalized;
        GetComponent<Rigidbody2D>().AddForce(moveDir * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        Destroy(gameObject, 14);
    }

    public void Explode()
    {
        GameObject[] asteroids = { Instantiate(asteroidPrefab, transform.position, Quaternion.identity), Instantiate(asteroidPrefab, transform.position, Quaternion.identity) };
        foreach (GameObject asteroid in asteroids)
        {
            asteroid.GetComponent<Asteroid>().Health = Health - 1;
            asteroid.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        OnAsteroidExploding?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(bulletTag))
        {
            Destroy(collision.gameObject);
            Explode();
        }
    }
}
