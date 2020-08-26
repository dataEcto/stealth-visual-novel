using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;
using System.Security.Cryptography;

public class GameEnvironment : MonoBehaviour
{
    //This script will be used to grab the Checkpoints on the scene.
    //A lot of new code, will need to comment on it.
    //Itll be nice to have a singleton to call upon without having to like, put it in the inspector or something.
    //private static GameEnvironment instance;
    
    //An empty list of checkpoints thats set to be private
    private List<GameObject> checkpoints = new List<GameObject>();
    private GameObject npc;

    
    //Is this where we publicly get the list from?
    public List<GameObject> Checkpoints 
    {
        get { return checkpoints; }
    }
    
    

    /*public static GameEnvironment Singleton
    {
        get
        {
           //If instance is empty, then we will create the instance
            if (instance == null)
            {
                instance = new GameEnvironment();
                //Populate the checkpoints for the instance.
                //However, it seems to be getting the waypoints and putting them in the list randomly.
                //Imagine it as though you grab the checkpoints from your hands and quickly put them in a box without regard for order.
                //That seems to be happening right now by using FindGameObjectsWithTag;
                //So in the ai it creates a random patrol pattern, but it doesnt seem to be as dynamic as the navmesh agent done by Tableflip games
                instance.Checkpoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));

                //We can order the waypoints in order by alphabet here
                //make sure to use the system.Linq namespace though.
                instance.checkpoints = instance.checkpoints.OrderBy(waypoint => waypoint.name).ToList();
            }

            //And then return it.
            return instance;
        }
    }*/
    
    //Why is this whole part commented out?
    //That is because before this script used to be a static class that couldnt be attached to a gameobject
    //It would have initally grabbed all the waypoints from within the script to work
    //but thats the problem...it doesnt have a way to allow multiple guards to have different checkpoints

    public void Start()
    { npc = this.gameObject;
       Checkpoints.AddRange(GameObject.FindGameObjectsWithTag(npc.GetComponent<AI>().waypointTag));

        //We can order the waypoints in order by alphabet here
        //make sure to use the system.Linq namespace though.
        checkpoints = checkpoints.OrderBy(waypoint => waypoint.name).ToList();
    }
}
