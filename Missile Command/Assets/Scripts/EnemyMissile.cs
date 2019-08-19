﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    [SerializeField] private GameObject explosionPrefab;

    private GameObject[] defenders;

    private float m_Angle;

    // The transform of defenders
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0, defenders.Length)].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);


        // Missile always toward the target
        Vector2 direction = target - transform.position;
        m_Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(m_Angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Defenders"))
        {
            MissileExpose();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Explosions"))
        {
            // Add scores when successfully destroyed an enemy missile
            FindObjectOfType<GameController>().AddMissileDestroyedPoints();
            MissileExpose();
           // Destroy(collision.gameObject);
        }
    }

    private void MissileExpose()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }

    void OnGUI()
    {
        //Output the angle found above
       // GUI.Label(new Rect(25, 25, 200, 40), "Angle Between Objects" + m_Angle);
    }
}