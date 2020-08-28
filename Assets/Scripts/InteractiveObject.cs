using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class InteractiveObject : MonoBehaviour
{
    //This script is present on all interactiveable objects.
    
    //Get access to the fungus flowchart
    public Flowchart interactiveChart;
    //as well as the playerMovement script, so t
    public PlayerMovement controlScript;

    //Lets us name the block we want to run from inspector
    //prevents the need to make a million script copies luckily for us!
    public string BlockToExecute;


    //This function is called upon by PlayterInteract on line if (Input.GetKeyDown(KeyCode.E)
    public void DoInteraction()
    {
        interactiveChart.ExecuteBlock(BlockToExecute);
    } 

}
