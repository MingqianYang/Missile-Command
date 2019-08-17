using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private  GameObject missilePrefab;
    [SerializeField] private float yPadding = 0.5f;
    private float minX, maX;


    // Start is called before the first frame update
    void Start()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x; 
        maX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float xValue = Random.Range(minX, maX);
        float yValue  = minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        Instantiate(missilePrefab, new Vector3(xValue, yValue + yPadding, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
