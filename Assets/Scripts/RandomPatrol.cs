using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrol : State
{
      //Dictates whether or not the agent waits on each waypoint/patrol point/node
      [SerializeField] private bool _patrolWaiting;
      
      //the total time we wait at each node. can be adjusted
      [SerializeField] private float _totalWaitTime = 3f;
      
      //the probability of switching direction
      //[SerializeField] private float _switchProbability = 0.2f;
      
      private ConnectedWaypoint _currentWayPoint;
      private ConnectedWaypoint _previousWayPoint;
      
      private bool _traveling;
      private bool _waiting;
      private bool _patrolForward;
      private float _waitTimer;
      private int _waypointsVisited;
      
    public RandomPatrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag) : base(_npc, _agent,
        _anim, _playerTransform, _spottedPlayer, _waypointTag)
    {
          //give the state a name again
          name = STATE.RANDOMPATROL;
          
          //and set the nav mesh agent going
          //and make sure it doesnt stop till it get to the next point.
          //these things only get set when the npc needs to get moving
          agent.speed = 3f;
          agent.isStopped = false;
    }
      


   public override void Enter()
   {

       _patrolWaiting = true;
       
       if (_currentWayPoint == null)
        {
            //We need to set the current waypoint to go to to be random.
            //So, we need to grab all the waypoints in the scene.
            //Should do something such as grabbing the waypoint gameobject, and then getting the array from there.
            GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag(waypointTag);

            if (allWayPoints.Length > 0)
            {
                //and 
                while (_currentWayPoint == null)
                {
                    //make up a random number to search in the array
                    int random = Random.Range(0, allWayPoints.Length);
                    //Then check if the waypoints have a ConnectedWayPoint 
                    ConnectedWaypoint startingWayPoint = allWayPoints[random].GetComponent<ConnectedWaypoint>();
                        
                    //now we have a waypoint
                    if (startingWayPoint != null)
                    {
                        _currentWayPoint = startingWayPoint;
                    }
                }
            }
            else
            {
                {
                    Debug.LogError("No waypoints found in scene. Did you tag them properly?");
                }
            }
        }

        DetermineDestination();
            
        anim.SetTrigger("isWalking");
       
        base.Enter();
   }

   public override void Update()
   {
       //Check if we are close to the patrol point.
       if (_traveling && agent.remainingDistance <= 1.0f)
       {
           //If we are close, set traveling to false.
           //_traveling will be set to true again by DetermineDestionation
           _traveling = false;
           //Maintain a count of all waypoints visited
           _waypointsVisited++;

           //If we need to wait, then start to do so here.
           if (_patrolWaiting)
           {
               _waiting = true;
               //The wait timer gets reset back to 0 here
               //Getting ready to count up again.
               _waitTimer = 0f;
           }
           else
           {
               DetermineDestination();
           }
       }

       //If we're waiting, do the following
       if (_waiting)
       {
           //anim.ResetTrigger("isWalking");
           //anim.SetTrigger("isIdle");
           
           Debug.Log("Should be waiting");
           //Increase the wait timer.
           //Similar to the john lemon tutorial, this focuses on a timer that goes up instead of down.

           _waitTimer += Time.deltaTime;
           if (_waitTimer >= _totalWaitTime)
           {
               _waiting = false;

               //anim.ResetTrigger("isIdle");
               //anim.SetTrigger("isWalking");
               
               DetermineDestination();

           }
       }

       if (CanSeePlayer())
       {
           //We set it to pursue first and then go to attack, i dont think itll make a difference
           nextState = new Pursue(npc, agent, anim, playerTransform,spottedPlayer,waypointTag);
           stage = EVENT.EXIT;
       }

   }
   
   public override void Exit()
   {
       //anim.ResetTrigger("isWalking");
       base.Exit();
   }
   
       
   public void DetermineDestination()
   {
       //Another check to see if null
       if (_waypointsVisited > 0)
       {
           //Give me your next waypoint using NextWayPoint from the ConnectedWaypoint script;
           ConnectedWaypoint nextWayPoint = _currentWayPoint.NextWayPoint(_previousWayPoint);
           //heres the one i just visited
           _previousWayPoint = _currentWayPoint;
           //And once you found one, set the current one to be the new one.
           //A classic switcheroo, and you'll always have an active reference to where you have been and where you are going.
           _currentWayPoint = nextWayPoint;
       }

       Vector3 targetVector = _currentWayPoint.transform.position;
       agent.SetDestination(targetVector);
       _traveling = true;
   }
}
