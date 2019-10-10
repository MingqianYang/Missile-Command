using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    // The speed of enemy missile
    [SerializeField] private float speed = 5f;

    // The reference of explosion prefab
    [SerializeField] private GameObject explosionPrefab;

    // the houses on the ground
    private GameObject[] defenders;
    private GameController myGameController;

    // The postion of defenders
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameController.FindObjectOfType<GameController>();

        defenders = GameObject.FindGameObjectsWithTag("Defenders");

        // Randomly get one defender's position
        target = defenders[Random.Range(0, defenders.Length)].transform.position;

        speed = myGameController.enemyMissileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enmey missile to the defender's position
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // the arrow point of the missile always toward the target
        Vector2 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        // If the emeny missile moved to the defenders' position, then destory enemy missile
        if (transform.position.Equals(target))
        {
            myGameController.EnemyMissileDestroyed();
            MissileExpose();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This enemy missile successfully destoryed the defender houses on the ground  
        if (collision.CompareTag("Defenders"))
        {
            myGameController.EnemyMissileDestroyed();
            MissileExpose();
            if (collision.GetComponent<MissileLauncher>() != null)
            {
                // Subtract player missiles when launcher hit (penalty)
                myGameController.MissileLauncherHit();
                return;
            }

            myGameController.cityCounter--;

            Destroy(collision.gameObject);
        }
        // This enmey missile is intercepted/destoryed by player's missile
        else if (collision.CompareTag("Explosions"))
        {
            // Add scores when successfully destroyed an enemy missile
            myGameController.AddMissileDestroyedPoints();
            MissileExpose();          
        }
    }

    // Spwn explosion prefab, and destory this enemy missile
    private void MissileExpose()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);        
    }

}
