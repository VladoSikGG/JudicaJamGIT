using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : BotInterface
{
    public Vector3 des;

    // Update is called once per frame
    void Update()
    {
        GameObject allyShip = FindClosestAllyShip();
        //Debug.Log(Vector3.Distance(transform.position, _player.position));
        if (distanceToSee > Vector3.Distance(transform.position, allyShip.transform.position) && distanceToAttack < Vector3.Distance(transform.position, allyShip.transform.position))
        {
            RotateToTarget(allyShip.transform.position);
            GoToTarget(allyShip.transform.position);
            Debug.Log(allyShip.transform.position);
            Debug.Log("gotoplayer");
        }
        else if (distanceToAttack > Vector3.Distance(transform.position, allyShip.transform.position) && canFire)
        {
            //attack
            Attack(allyShip.transform.position);
            Debug.Log("attack");
        }
        else if (distanceToSee < Vector3.Distance(transform.position, allyShip.transform.position))
        {
            //patrol
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
            
        }
    }
}
