using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{

    // The position of the mouse click point on the screen
    private Vector2 target;

    // The speed of player missile
    [SerializeField] private float speed = 5f;

    // The explosion effect object
    [SerializeField] private GameObject explotionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        // Get mouse posiiton on the screen and convert it to world
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
    }

    // Update is called once per frame
    void Update()
    {
        // Move the missile to the targeted position
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Missile always toward the target
        Vector2 direction = (Vector3)target - transform.position;
        float m_Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(m_Angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);

        // Generate explosion prefab when the player missile moved to the target position
        if (transform.position == (Vector3) target )
        {
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
