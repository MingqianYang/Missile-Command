using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
    private Vector2 target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explotionPrefab;

    private float m_Angle;

    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Missile always toward the target
        Vector2 direction = (Vector3)target - transform.position;
        m_Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(m_Angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);


        if (transform.position == (Vector3) target )
        {
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
