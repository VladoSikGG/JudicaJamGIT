using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : BotInterface
{
    [SerializeField] private Transform _player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, _player.position));
        if (distanceToSee > Vector3.Distance(transform.position, _player.position) && distanceToAttack < Vector3.Distance(transform.position, _player.position))
        {
            GoToTarget(_player);
            Debug.Log("gotoplayer");
        }
        else if (distanceToAttack > Vector3.Distance(transform.position, _player.position))
        {
            //attack
            Debug.Log("attack");
        }
        else
        {
            //patrol
            Debug.Log("patrol");
        }
    }
}
