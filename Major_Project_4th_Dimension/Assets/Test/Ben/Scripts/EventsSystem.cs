using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Allows for the eventsystem to trigger a specific action being it be exit or entering.
/// Needsd to be "destroyed" once the action is deleted or not in use anymore.
/// </summary>
public class EventsSystem : Singleton<EventsSystem>
{
    public event Action<int> triggerEnter;
    public void TriggerEnter(int id) 
    {
        if (triggerEnter != null)
            triggerEnter(id);
    }
    public event Action<int> triggerExit;
    public void TriggerExit(int id)
    {
        if (triggerExit != null)
            triggerExit(id);
    }
}
