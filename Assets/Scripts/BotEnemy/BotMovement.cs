using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : BotInterface
{
    [SerializeField] private Transform _player;
    public Vector3 des;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, _player.position));
        if (distanceToSee >= Vector3.Distance(transform.position, _player.position) && distanceToAttack < Vector3.Distance(transform.position, _player.position))
        {
            GoToTarget(_player.position);
            Debug.Log("gotoplayer");
            isPatrol = false;
        }
        else if (distanceToAttack >= Vector3.Distance(transform.position, _player.position) && canFire)
        {
            //attack
            Attack();
            Debug.Log("attack");
        }
        else if (distanceToSee < Vector3.Distance(transform.position, _player.position))
        {
            Debug.Log("Patrol");
           //when enemy not near go to point
            if (!isPatrol)
            {
                //create new point and set destination
                isPatrol = true;
                des = NewDestination();
                GoToPatrolPoint(des);
            }

            //if enemy near the point he is stop
            if (CheckDistance(des))
            {
                isPatrol = false;
            }
            //patrol
        }
        {
           
        }
        
    }
}
