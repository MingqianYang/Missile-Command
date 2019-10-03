using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PrintActiveMessage()
    {
        print("OK");
        Debug.Log("OK");
    }
    public void PrintDeactiveMessage()
    {
        print("NO");
        Debug.Log("NO");
    }
}
