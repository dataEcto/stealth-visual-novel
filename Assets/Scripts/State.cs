using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

public class State 
{
    /*//Ages ago, I remember Rilee was making a game and was asking Charles for help regarding code.
    //Charles then suggested using an enum, which I never actually learned in school.
    //Probably should have swapped out some of the intermediate visual design stuff with coding, but what can ya do?

    //So here is the script and what it does, courtesy of Unity Learn.
    //This is a State class, and all behaviors of our AI derive from here.
    //We declare the states first.
    //We then declare the events that occur in each state, such as when it enters the state, how the state should run, and how it should exit the state
    //We properly declare are enums so that they can be called, and we also create the variables for the game object and the components the guard has so that we can utilize it.
    //We also declare a constructor that provides each state the components needed to function
    //and finally we also make a method that will help us go through each Event enum.*/
    
    //We first have to declare what our states are. We do this by making an Enum.
    public enum STATE
    {
        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        RANDOMPATROL,
    };

    //We also make an enum of what happens in each state, called an Event.
    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };
    
   //First, lets properly declare our enums to be called in.
    public STATE name;
    protected EVENT stage;
    
    //And then we create the generic game objects each state will need and then hold.
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator anim;
    protected Transform playerTransform;
    protected bool spottedPlayer;
    protected string waypointTag;
    
    //Note that this state delcaration is not the same as the enum
    //This just holds the next state for us
    protected State nextState;
    //We could use a State to keep a previousState option
    
    
    //Guard variables. These are temp though, because I want to cast a raycast and use a capsule collider on a game object instead of 
    //straight numbers.
    //private float visDist = 10f;
    //private float visAngle = 30f;
    private float shootDist = 7.0f;
    
    
    //This is a constructor.
    //Constructors allow us to prepare an object before we even use it/
    //At their core, constructors are specialized methods. So no, the "State" before the parentheses is not the name, its just name of the strut/return type.
    //In each state script, you can see we're using this constructor for each one!
    //Its basically an easy way for each state to get access to variables from our guard NPC without having to be within the script. 
    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag)
    {
        //Here we provide the instance variables.
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        playerTransform = _playerTransform;
        spottedPlayer = _spottedPlayer;
        waypointTag = _waypointTag;

        //Should probably include here a reference to a trigger that serves as the pov, or whatever ends up being the way i make a Guard FOV.
        //Of course theres the problem where, if I were to create the guard fov in a child game object, i need to get the guard to take it from the child gameobject
    }
    
    //This method uses 3 Methods declared below 
    //And help progress the AI through each stage event.
    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }

        if (stage == EVENT.UPDATE)
        {
            Update();
        }

        if (stage == EVENT.EXIT)
        {
            Exit();
            //this method will actually return a state
            return nextState;
        }

        //This only gets called if next state doesnt get returned.
        return this;
    }
    
    //Now lets put in skeleton/base code for each of our phases as we go through the state
    //These mainly serve to help transistion between states/get them to work properly.
    public virtual void Enter()
    {
        //Once we start using this method, the stage will then be set to what it will be next-update.
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        //You'll want to stay in update until update itself throws it out
        stage = EVENT.UPDATE;
    }
    public virtual void Exit()
    {
        //It is what it is
        stage = EVENT.EXIT;
    }

    //Below are some methods that are to be called by more than one state, either to transistion to said method or to activate it
    //This is why they're in here, because it'll be easy to get access to it from here rather than having to copy and paste it in every script.
    //Of course, not all methods need be here...some methods will only ever be used by one state
    
    //Creating a line of sight for the NPC to see the player.
    //This is for the Pursuit stage
    public bool CanSeePlayer()
    {
        //give us the vector from the npc to the player
        //Future june here
        //ugh this is really weird because it seems as though playertransform position is always getting updated
        //yet when i try to do the same thing with the spotted bool, nada happens.
        //maybe i try again tommorow
        Vector3 direction = playerTransform.position - npc.transform.position;
        
        //Create the angle to figure out if the player is in the line of sight
        float angle = Vector3.Angle(direction, npc.transform.forward);
        //Remember visDist and visAngle is the float that determines the visible distance
        if (npc.GetComponent<FieldOfView>().visibleTargets.Count > 0)
        {
            //Its the enemy!
            return true;
        }
        else
        {
            return false;
        }

        //Must have been my imagination...
        
    }
    
    //For the attack stage.
    public bool CanAttackPlayer()
    {
        Vector3 direction = playerTransform.position - npc.transform.position;
        if (direction.magnitude < shootDist)
        {
            return true;
        }

        return false;
    }






}



    


   
