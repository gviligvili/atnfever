using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpsSpawner : MonoBehaviour {

    public float width;
    public float height;
    public PowerUpsEnum PowerUpTypes;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

	// Use this for initialization
	void Start () {

        // Spawn every X time
        Spawn();


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Spawn() {
        // Get random XY within the spawner range.
        Vector2 pos = getRandomXY();
    }


    Vector2 getRandomXY()
    {
        float halfHeight = height / 2;
        float halfWidth = width / 2;

        float x = Random.Range(-halfWidth, halfWidth);
        float y = Random.Range(-halfHeight, halfHeight);

        return new Vector2(x, y);
    }
}
