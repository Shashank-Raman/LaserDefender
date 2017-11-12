using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float health = 150.0f;
    public float projectileSpeed = 0.0f;
    public GameObject projectile;
    public float shotsPerSecond = 1.0f;
    public int scoreValue = 150;

    private ScoreKeeper scoreKeeper;

    public AudioClip fireSound;
    public AudioClip deathSound;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile laser = collider.gameObject.GetComponent<Projectile>();
        if (laser != null)
        {
            //Debug.Log("Collision detected");
            laser.Hit();
            health -= laser.GetDamage();
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        scoreKeeper.Score(scoreValue);
        Destroy(gameObject);
    }
}
