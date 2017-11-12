using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 15.0f;
    public float padding = 1.0f;
    public float projectileSpeed = 0.0f;
    public GameObject projectile;
    public float firingRate = 0.2f;
    public float health = 250.0f;

    private float xMin;
    private float xMax;

    public AudioClip fireSound;

	// Use this for initialization
	void Start () 
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftmostPosition.x + padding;
        xMax = rightmostPosition.x - padding;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			//transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
		}

        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);	
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

    void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }

    void Die()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().LoadLevel("Win Screen");
        Destroy(gameObject);
    }
}
