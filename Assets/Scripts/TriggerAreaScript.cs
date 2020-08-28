using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter.Debugging;
using UnityEngine;
using Fungus;

public class TriggerAreaScript : MonoBehaviour
{
    public int id;
    public Flowchart interactiveChart;
    
    
    //So from GameEvents to DoorController to this very script, we managed to call a function without having to make too many singleton references!
    //Or something like that.

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        //If the flowchart has the allow door to be open variable, then let it open when pressing E
        //If not, call a block that says "I cant open this door rn"
        if (Input.GetKeyDown(KeyCode.E) && interactiveChart.GetBooleanVariable("EnableDoorOpen"))
        {
            GameEvents.current.DoorwayTriggerEnter(id);
            Debug.Log("door open");
     
        }
        
        if (Input.GetKeyDown(KeyCode.E) && interactiveChart.GetBooleanVariable("EnableDoorOpen") == false)
        {
            interactiveChart.ExecuteBlock("Door Blocked");
        }

        if ((Input.GetKeyDown(KeyCode.E) && interactiveChart.GetBooleanVariable("EnableDoorOpen") && this.gameObject.tag == "door 2"))
        {
            interactiveChart.ExecuteBlock("Door Blocked");
        }
        
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameEvents.current.DoorwayTriggerExit(id);
        Debug.Log("door closed");
    }
    
    
}
