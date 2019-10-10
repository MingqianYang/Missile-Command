using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    // The missile prefab
    [SerializeField] private  GameObject missilePrefab;

    // When spawn the missile, put it a little bit upper on the toppest view point
    [SerializeField] private float yPadding = 0.5f;

    // The x cordinate value of left up corner and the right up corner of the viewpoint
    private float minX, maX;
    
    // The y cordinate value
    private float yValue;

    // How many enemy missiles will be spawned in this round
    public int missilesToSpawnThisRound { get; set; }

    // The time gap between two generated missiles
    public float timeBetweenMissiles = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x; 
        maX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        StartCoroutine(SpawnMissiles(timeBetweenMissiles));
    }

    IEnumerator SpawnMissiles(float time)
    {
        while (missilesToSpawnThisRound > 0)
        {
            // Randomly genertate the x posiiton
            float xValue = Random.Range(minX, maX);

            Instantiate(missilePrefab, new Vector3(xValue, yValue + yPadding, 0), Quaternion.identity);

            missilesToSpawnThisRound--;

            yield return new WaitForSeconds(time);
        }
    }
}
