using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
  //This script will draw a gizmo that shows the waypoints in game

  [SerializeField] protected float debugDrawRadius = 1.0f;

  public virtual void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
  }
}
