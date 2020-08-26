using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConnectedWaypoint : Waypoint
{
    //A class that has a method that can check for a waypoint near the npc.
    //This makes it so you dont have to put an array list onto the scene, and the npc just follows whatever waypoints are either in the scene or based on a specific tag
    //This is code to visualize the connected waypoints.
    //it is a subclass of the waypoint script.
    
    [SerializeField] protected float _connectivityRadius = 50f;

    private List<ConnectedWaypoint> _connections;

    [SerializeField] protected string waypointTag;
    
    void Start()
    {
        //First lets grab all the waypoint objects that are in the scene right now.
        GameObject[] allWayPoints = GameObject.FindGameObjectsWithTag(waypointTag);
        //Got to remember to tag
        
        //Then create a list of waypoints for reference
        //Right now it is empty.
        _connections = new List<ConnectedWaypoint>();
        
        //Check if they're a connected wavepoint
        for (int i = 0; i < allWayPoints.Length; i++)
        {
            ConnectedWaypoint nextWaypoint = allWayPoints[i].GetComponent<ConnectedWaypoint>();
            
            //if a waypoint was found. This is just to doublecheck.
            if(nextWaypoint !=null)
            {
                if (Vector3.Distance(this.transform.position, nextWaypoint.transform.position) <=
                    _connectivityRadius && nextWaypoint != this)
                {
                    _connections.Add(nextWaypoint);
                }
            }
        }
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,debugDrawRadius);

        /*Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,_connectivityRadius);*/
    }

    //Method to be used in the actual patrol code
    //Checks if there is a way point to reach around the player.
    public ConnectedWaypoint NextWayPoint(ConnectedWaypoint previousWaypoint)
    {
        //If there is no waypoints, return null
        //This also includes scripts that are looking for waypoints of a certain tag too!
        if (_connections.Count == 0)
        {
           
            Debug.LogError("Insufficient Waypoint Count. Don't forget to tag them CORRECTLY");
            return null;
        }
        //If there is only 1 wavepoint, and it's just the previous one, Just use that one. 
        //It is the only one you can reach from here.
        else if (_connections.Count == 1 && _connections.Contains(previousWaypoint))
        {
            
            return previousWaypoint;
        }
        //Otherwise, find a random waypoint that isn't the previous one.
        else
        {
            ConnectedWaypoint nextWaypoint;
            int nextIndex = 0;

            
            do
            {
                //Generate a random number
                nextIndex = UnityEngine.Random.Range(0, _connections.Count);
                //and then use that number to reference another waypoint
                nextWaypoint = _connections[nextIndex];
            } while (nextWaypoint == previousWaypoint); //This while loop only works if nextWaypoint ends up being equal to itself.

            return nextWaypoint;
        }
    }


}
