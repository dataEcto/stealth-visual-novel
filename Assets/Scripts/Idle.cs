using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    //This whole script is based off the constructor that was created in State.
    
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag) : base(_npc, _agent,
        _anim, _playerTransform, _spottedPlayer, _waypointTag)
    {
        //Now we give our state a name
        //we're using the name variable that's in the state class.
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        //anim.SetTrigger("isIdle");
        //Now we can call the Enter method that we used in our State Script to start our enter state.
        base.Enter();
    }

    public override void Update()
    {
        //There isn't actually much else to do during this state, which is why this part of the script only focuses on going to the next states.
        
        //We use the method from the state script to check if we can see the player.
        if (CanSeePlayer() )
        {
            //If we can, then let us start going to the next state.
            nextState = new Pursue(npc, agent, anim, playerTransform, spottedPlayer, waypointTag);
            //and make sure we exit out of it too! 
            //I guess a good analogy is closing the door on the way out.
            stage = EVENT.EXIT;
        }
        
        //No method from the state script this time. just making it so theres a 10 percent chance the guard will start patroling.
        else if (UnityEngine.Random.Range(0, 100)  < 10 )
        {
          
            nextState = new Patrol(npc, agent, anim, playerTransform, spottedPlayer,waypointTag);
            stage = EVENT.EXIT;
        }

        if (npc.tag == "Random")
        {
            nextState = new RandomPatrol(npc, agent, anim, playerTransform, spottedPlayer, waypointTag);
        }
        
        //We dont need to call base.Update because it kind of loops us into update no matter how many times we call for event.exit;
        
    }

    public override void Exit()
    {
        //Make sure to reset trigger so that the animation doesnt interfere with any of the states.
        //anim.ResetTrigger("isIdle");
        base.Exit();
    }
}
