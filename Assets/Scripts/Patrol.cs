using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : State
{
    //Patrols rely on finding waypoints.


    //A reference number to our Checkpoints list
    private int currentIndex = -1;
    
    
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag) : base(_npc, _agent,
        _anim, _playerTransform, _spottedPlayer, _waypointTag)
    {
        //give the state a name again
        name = STATE.PATROL;
        //and set the nav mesh agent going
        //and make sure it doesnt stop till it get to the next point.
        //these things only get set when the npc needs to get moving
        agent.speed = 2f;
        agent.isStopped = false;
    }
    
   
     //Once we enter this state, we need to get our waypoints list.
     //We dont need to save our waypoints into anything- the GameEnvironment singleton will handle that
     //However, if I were to overrwrite the bottom part with something based off Tableflips patrol...Could it work?
    public override void Enter()
    {
        //We don't want to start the currentindex from -1 cuz the list doesnt have a -1
        //I do wonder why we don't set it to 0 to begin with though
        //currentIndex = 0;
        
        //We can also make it so that it goes to the neareset waypoint instead of the 0 of the index
        
        //First store the distance from the waypoints and the npc, and then remember the smallest
        /*//First we should set it as a big number at the start
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            //If the distance just calculated is smaller than the last distance, then surely this current waypoint must be closer.
            if (distance < lastDist)
            {
                //we know we're getting the closet waypoint depending on the index, but the gaurd will end up going to the one ahead of it
                //since currentIndex in update gets added by 1
                //So we need to fix this a bit to make it minus 1 and then the plus 1 in update will get us back to the normal number.
                currentIndex = i -1;
                lastDist = distance;
            }
        }*/
        
        
        //We are trying something new
       
          
        //anim.SetTrigger("isWalking");
        
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            //If we're on the last checkpoint, lets reset back to the start
            if (currentIndex >= npc.GetComponent<GameEnvironment>().Checkpoints.Count - 1)
            {
                currentIndex = 0;
            }
            //Otherwise lets keep going
            else
            {
                currentIndex++;
            }

            //Then lets set the destination now
            agent.SetDestination(npc.GetComponent<GameEnvironment>().Checkpoints[currentIndex].transform.position);
        }
        
        if (CanSeePlayer())
        {
            //We set it to pursue first and then go to attack, i dont think itll make a difference
            Debug.Log("Should be pursuing");
            nextState = new Pursue(npc, agent, anim, playerTransform, spottedPlayer, waypointTag);
            stage = EVENT.EXIT;
        }

        //Again we dont need to call base.update'

        
      
    }

    public override void Exit()
    {
        //anim.ResetTrigger("isWalking");
        base.Exit();
    }


}
