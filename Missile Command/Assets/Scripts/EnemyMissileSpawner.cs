using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private  GameObject missilePrefab;
    [SerializeField] private float yPadding = 0.5f;
    private float minX, maX;

    public int missilesNumbersOfThisRound = 10;
    public float timeBetweenMissiles = 0.5f;

    float yValue;
    // Start is called before the first frame update
    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x; 
        maX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        StartCoroutine(SpawnMissiles(timeBetweenMissiles));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnMissiles(float time)
    {
        while (missilesNumbersOfThisRound > 0)
        {
            float xValue = Random.Range(minX, maX);


            Instantiate(missilePrefab, new Vector3(xValue, yValue + yPadding, 0), Quaternion.identity);

            missilesNumbersOfThisRound--;

            yield return new WaitForSeconds(time);
        }
    }
}
