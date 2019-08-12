using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICustomMessageTarget : IEventSystemHandler
{
    // Functions that can be called via the messging system
    void Message1();
    void Message2();
}
