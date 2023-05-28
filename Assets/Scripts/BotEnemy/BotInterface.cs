using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotInterface : MonoBehaviour
{
    //AI
    [Header("AI")]
    [SerializeField] private NavMeshAgent _agent;
    //for movement
    [Header("For Movement")]
    [SerializeField] public float distanceToAttack;
    [SerializeField] public float distanceToSee;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _distanceForPatrol;
    //for entaraction
    [Header("For Fight")]
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeForReload;

    public bool canFire = true;
    [SerializeField]private LineRenderer _lineRender;
    //public bool _havePoint;
    public bool isPatrol;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lineRender = GetComponent<LineRenderer>();
    }

    public void GoToTarget(Vector3 target)
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, _speed);
        RotateToTarget(target);
        _agent.Move(transform.TransformDirection(Vector3.forward * _speed));
    }

    public void RotateToTarget(Vector3 target)
    {
        var targetRotation = Quaternion.LookRotation(new Vector3(target.x, target.y, target.z) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed);
    }

    public void GetDamage(int value)
    {
        _hp -= value;
        if (_hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public Vector3 NewDestination()
    {
        //create new point
        return new Vector3(transform.position.x + Random.Range(-_distanceForPatrol, _distanceForPatrol), 0, transform.position.z + Random.Range(-_distanceForPatrol, _distanceForPatrol));
    }

    public bool CheckDistance(Vector3 destination)
    {
        return Vector3.Distance(transform.position, destination) < 0.5f;
    }

    public void GoToPatrolPoint(Vector3 destination)
    {
        // go to point
        RotateToTarget(destination);
        _agent.SetDestination(destination);
    }

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceToAttack + 1f))
        {
            if (hit.collider.tag == "Player")
            {
               // hit.collider.GetComponent<AllyInterface>().GetDamage(_damage);
                StartCoroutine(Reloading());
                //laser
                _lineRender.SetPosition(0, transform.position);
                _lineRender.SetPosition(1, hit.collider.transform.position);
            }
            Debug.Log(hit);
            
        }
        
    }
    IEnumerator Reloading()
    {
        canFire = false;
        yield return new WaitForSeconds(_timeForReload);
        canFire = true;
    }
}
