using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10.0f;
    public float height = 5.0f;

    public float speed = 5.0f;
    public bool movingRight = true;

    private float xMin;
    private float xMax;

    public float spawnDelay = 0.2f;

    private Vector3 leftmostPosition;
    private Vector3 rightmostPosition;

	// Use this for initialization
	void Start () 
    {
        SpawnEnemies();
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        leftmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        rightmostPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftmostPosition.x;
        xMax = rightmostPosition.x;
	}
        
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    void SpawnEnemies()
    {
        Transform nextFreePosition = NextFreePosition();
        if (nextFreePosition != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextFreePosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextFreePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnEnemies", spawnDelay);
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdge = transform.position.x + (0.5f * width);
        float leftEdge = transform.position.x - (0.5f * width);

        if (leftEdge < xMin)
        {
            movingRight = true;
        }
        else if (rightEdge > xMax)
        {
            movingRight = false;
        }

        if (AllEnemiesAreDead())
        {
            //Debug.Log("All my friends are dead");
            SpawnEnemies();
        }
	}

    Transform NextFreePosition()
    {
        foreach (Transform childGameObject in transform)
        {
            if (childGameObject.childCount == 0)
            {
                return childGameObject;
            }
        }
        return null;
    }

    bool AllEnemiesAreDead()
    {
        foreach (Transform childGameObject in transform)
        {
            if (childGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
