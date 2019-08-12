using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMessageTarget : MonoBehaviour, ICustomMessageTarget
{

    public void Message1()
    {
        Debug.Log("messge 1 received");
    }


    public void Message2()
    {
        Debug.Log("messge 2 received");
    }

    public void SendCustomMessage()
    {
        ExecuteEvents.Execute<ICustomMessageTarget>(target, null, (x, y) => x.Message1());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
