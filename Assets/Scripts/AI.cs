using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    //This script is where we get all of the game components needed to make the other states run!
    //In fact, that is mostly the main purpose of this script.
    private NavMeshAgent agent;
    private Animator anim;
    //We could put the player in the guards inspector, but maybe we can also do a find object of type/find with a tag
    public Transform playerTransform;

    private State currentState;
    
    
    [SerializeField] public string waypointTag;

    private FieldOfView fovScript;
    
    //Something to note: If I wanted to make different types of Guard AI, how could I do that? 
    //you can just put a condition within the finite state machine that checks what kind of game object the npc is. if its been tagged as a special kind, it would run that specalized state instead.
    //You may not even needed to make a varaible that checks the tag-could do something like npc.tag
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent <Animator>();
        fovScript = this.gameObject.GetComponent<FieldOfView>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
      

        //And here is where everything starts! By setting the currentState to be idle.
        //This is also where we declare the npc game object for all of the states to use.
        //waypoint tag's string is seen in the inspector
        currentState =new Idle(this.gameObject,agent,anim,playerTransform,fovScript.isSpotted,waypointTag);
        
    }
    
    // Update is where the magic happens.
    //But has the potential for spagethi code
    //States help keep things clean
    void Update()
    {
        //Remember, the Process method works through each of the stages
        //So to recap whats going on:
        //First we get our current state, which was assigned to be idle here, and make it call the Process method.
        //The Process method will run, and check what the current stage is. 
        //If its in the enter stage, it runs a method we created called Enter, which allows it to start being in various states.
        //The next time the Process method is called, which in this case will be in the next frame, it will run the State Update method .
        //Itll keep doing that, until update reaches a point in where it must enter the exit state, and then returns nextstate from there.
        
        //What properties next state has depends on what the Update function of each different state does.
        //for example, Update() in idle starts to transistion to Patrol, and Patrol itself has to follow a set of waypoints.
        currentState = currentState.Process();
        
        
        

    }

}
