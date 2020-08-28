using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private GameObject thisDoor;
    public int id;
    
    void Start()
    {
        //Add the OnDoorwayOpen to our GameEvents script!!
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        //Same with onDoorwayExit!
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayExit;
        thisDoor = this.gameObject;
    }


    private void OnDoorwayOpen(int id)
    {
        //This is a temp door opening code!
        //Maybe next time, when art is done, do something like
        //Play door opening animation, disable collider, than play door closed when player walks out of range?
        if (id == this.id)
        {
            thisDoor.SetActive(false);
        }
     
    }
    
    private void OnDoorwayExit(int id)
    {
        //This is a temp door closing code!
        if (id == this.id)
        {
            thisDoor.SetActive(true);
        }
       
    }
    
    //An extra method in case our doors get destroyed, just to avoid making it messy
    private void OnDestroy()
    {
        GameEvents.current.onDoorwayTriggerEnter -= OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit -= OnDoorwayExit;
        
    }
}
