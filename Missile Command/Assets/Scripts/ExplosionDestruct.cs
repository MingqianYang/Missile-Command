using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestruct : MonoBehaviour
{
    // The time period when the explosion visual effect will disapper
    [SerializeField]
    private readonly float destroyTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
