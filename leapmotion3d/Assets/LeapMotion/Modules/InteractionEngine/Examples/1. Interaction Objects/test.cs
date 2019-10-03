using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using LeapInternal;
using Leap.Unity;
public class test : MonoBehaviour
{
    Controller controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
    }

    // Update is called once per frame
    void Update()
    {
         // wait until Controller.isConnected() evaluates to true
        //...

        Frame frame = controller.Frame();

       

    }

    public void GraspBegin()
    {
        Debug.Log("begin");
    }

    public void GraspEnd()
    {
        Debug.Log("end");
    }
}
