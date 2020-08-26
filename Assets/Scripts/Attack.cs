using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    //In this case, the npc has to follow and turn and attack the player. we even get a reference to to the audio sourc here
    private float rotationSpeed = 2f;
    private AudioSource shoot;
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _playerTransform, bool _spottedPlayer, string _waypointTag) : base(_npc, _agent,
        _anim, _playerTransform, _spottedPlayer, _waypointTag)
    {
        name = STATE.ATTACK;
        //Because we get access to the npc, we can get its audio source.
        //shoot = npc.GetComponent<AudioSource>();
        //We could potentially get other components...something that does the shooting
        //  npc.GetComponent<AI>().TestMethod();
    }

    public override void Enter()
    {
        //anim.SetTrigger("isShooting");
        //Stop the agent from moving. only make them attack.
        agent.isStopped = true;
        //shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        //rotate the guard to face the player
        Vector3 direction = playerTransform.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        //We want the guard to rotate, but not tilt 
        //Any difference between the player and the npc should be 0 in turns of height.
        direction.y = 0; 
        
      
        
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed );
        
        //If we cant attack the player/the player is out of range
        if (!CanAttackPlayer())
        {
            //You could go to any state from here, such as pursue, patrol, idle, or if i wanted to, a sweep state
            //if you go to idle, it can go from any state from there.
            //The tutorial sets it to idle so that it can go to patrol and detect the player again.
            //However, they will admit they'll forget about you and go back to patrol, which i kind of want to avoid.
            nextState = new Idle(npc, agent, anim, playerTransform, spottedPlayer, waypointTag);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        //anim.ResetTrigger("isShooting");
        //shoot.Stop();
        base.Exit();
    }
}

