using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pursue : State
{
    
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag) : base(_npc, _agent,
        _anim, _playerTransform, _spottedPlayer, _waypointTag)
    {
        name = STATE.PURSUE;
        //Make the speed faster than patrol which makes him RUN
        agent.speed = 5;
        agent.isStopped = false;
    }
    
    

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    //The bulk of the pursue state will occur in here
    public override void Update()
    {
        agent.SetDestination(playerTransform.position);
        //hasPath is checking if there's actually a path.
        //Why do this? well, with navmesh, setting a destination can work, but the process of setting a destination might not have finished yet!
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                //Once we are in a reasonable distance from the player, then we can attack
                nextState = new Attack(npc, agent, anim, playerTransform, spottedPlayer, waypointTag);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                //If we can't see the player, we go back to patrol.
                //This could be a good way to set it up so that we can make the guard sweep the area.
                //lets follow the tutorial for now.
                nextState = new Patrol(npc, agent, anim, playerTransform,spottedPlayer, waypointTag);
                stage = EVENT.EXIT;
            }
        }
        
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}
