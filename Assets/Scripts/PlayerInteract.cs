using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    //This script handles what happens if a player interacts with an interactable object

    private GameObject currentIntObj = null;

    public PlayerMovement controlScriptMain;

    public Flowchart interactiveChart;
    
    //Whenever the player gets near an object that is interactable, a little E should pop up above Bushy
    //This script will control that
    public GameObject checkButton;
    
    void Start()
    {
        //E will always be disabled by default, but it will always follow Bushy around
        //It'll just be not active
        checkButton.SetActive(false);
    }
   
    void Update()
    {
        //When you press E and currentIntObj has a game object in it, do the interaction
        if (Input.GetKeyDown(KeyCode.E) && currentIntObj)
        {
            //Send a message to whatever the current object is to run the method DoInteraction
            currentIntObj.SendMessage("DoInteraction");
            //At this point, the player has already pressed E, so we can make the button disappear
            //Code is a bit nonlinear, so look down in the trigger collider part for more.
            checkButton.SetActive(false);
        }
   
        //This is where controls get enabled and disabled, being controlled by the flowchart.
        if (interactiveChart.GetBooleanVariable("enableControl") == true)
        {
            controlScriptMain.animator.enabled = true;
            controlScriptMain.enabled = true;
        }
        else
        {
            controlScriptMain.animator.enabled = false;
            controlScriptMain.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        
        //This is where currentIntObj has/gets its value
        //I actually can't remember if I got this from a youtube tutorial or if i made it
        if (other.CompareTag("InteractiveObject"))
        {
            currentIntObj = other.gameObject;
            Debug.Log(other.name);   
            //Make the Lil E show up when youre in the trigger area
            checkButton.SetActive(true);
            
        }
        
        //This is where the cutscene will be run by once.
        if (other.CompareTag("Cutscene") )
        {
            currentIntObj = other.gameObject;
            currentIntObj.SendMessage("DoInteraction");
            Debug.Log("Play a cutscene");   
        }
        
        if (other.CompareTag("door"))
        {
            //Make the Lil E show up when youre in the trigger area
            checkButton.SetActive(true);
        }
         
         if (other.CompareTag("door 2"))
         {
              checkButton.SetActive(true);
         }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractiveObject"))
        {
            if (other.gameObject == currentIntObj)
            {
                currentIntObj = null;          
            }
        }
        
        //If you leave the trigger area, make the e button dissapear
        checkButton.SetActive(false);
    }
}
