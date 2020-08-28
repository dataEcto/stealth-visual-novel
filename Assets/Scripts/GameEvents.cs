using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
  
  //First, we get a static singleton reference to this script.
  public static GameEvents current;

  public void Awake()
  {
    //Set the script to be itself
    current = this;
  }

  //We create an Action, which is a type of method that can hold and call multiple methods!
  //In this case though, we're making it hold one methods that have an int argument
  //that can be called from anywhere as long as the
  //GameEvents.current(name of method that is added) is called.
  //The actions start off empty.
  public event Action<int> onDoorwayTriggerEnter;

  public void DoorwayTriggerEnter(int id)
  {
    if (onDoorwayTriggerEnter != null)
    {
      onDoorwayTriggerEnter(id);
    }
  }
  
  //Same deal here.

  public event Action<int> onDoorwayTriggerExit;

  public void DoorwayTriggerExit(int id)
  {
    if (onDoorwayTriggerExit != null)
    {
      onDoorwayTriggerExit(id);
    }
  }
}
