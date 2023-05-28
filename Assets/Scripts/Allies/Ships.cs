using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ships : BotInterface
{
    [SerializeField] private Camera cam;
    private NavMeshAgent agent;
    public bool canMove;
    


    void Start()
    {
        SelectObjects.unit.Add(gameObject); // adding objects to an array of all units that we can select
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (canMove)
            MoveObject();

        GameObject enemyShip = FindClosestEnemyShip();
        if (Vector3.Distance(enemyShip.transform.position, transform.position) < distanceToAttack)
        {
            RotateToTarget(enemyShip.transform.position);
            Attack();
            
        }
        
    }

    void MoveObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //getting coordinates through a ray
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                if(hit.collider.tag != "Planet")
                agent.SetDestination(hit.point); //object movement
        }
    }

    
}
