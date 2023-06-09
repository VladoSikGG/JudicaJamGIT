using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotInterface : MonoBehaviour
{
    //AI
    [Header("AI")]
    [SerializeField] public NavMeshAgent agent;
    //for movement
    [Header("For Movement")]
    [SerializeField] public float distanceToAttack;
    [SerializeField] public float distanceToSee;
    [SerializeField] public float _speed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _distanceForPatrol;
    //for entaraction
    [Header("For Fight")]
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeForReload;
    [SerializeField] private float _timeForShot;
    [SerializeField]private GameObject[] _ships;
    private GameObject _closestShip;

    public bool canFire = true;
    [SerializeField] public LineRenderer _lineRender;
    //public bool _havePoint;
    public bool isPatrol;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
    }

    public void GoToTarget(Vector3 target)
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, _speed);
        agent.Move(transform.TransformDirection(Vector3.forward * _speed));
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
        SelectObjects.unit.Remove(gameObject);
        SelectObjects.unitSelected.Remove(gameObject);
        Destroy(gameObject);
        //removal from unitSelected

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
        agent.SetDestination(destination);
    }

    public void Attack(Vector3 target)
    {
        RotateToTarget(target);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceToAttack + 1f))
        {
            //when they stay in big group
            if (hit.collider.tag != this.gameObject.tag)
            {
                hit.collider.GetComponent<BotInterface>().GetDamage(_damage);
                StartCoroutine(Reloading());
                //laser
                _lineRender.SetPosition(0, transform.position);
                _lineRender.SetPosition(1, hit.collider.transform.position);
                this.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        
    }
    IEnumerator Reloading()
    {
        _lineRender.enabled = true;
        canFire = false;
        yield return new WaitForSeconds(_timeForShot);
        _lineRender.enabled = false;
        yield return new WaitForSeconds(_timeForReload - _timeForShot);
        canFire = true;
    }

    public GameObject FindClosestEnemyShip()
    {
        float lastDistance = Mathf.Infinity;
        _ships = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in _ships)
        {
            float between = Vector3.Distance(target.transform.position, transform.position);
            if (between < lastDistance)
            {
                _closestShip = target;
                lastDistance = between;
            }
        }
        return _closestShip;

    }
    public GameObject FindClosestAllyShip()
    {
        float lastDistance = Mathf.Infinity;
        Vector3 position= transform.position;
        _ships = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject target in _ships)
        {
            //float between = Vector3.Distance(target.transform.position, transform.position);
            Vector3 diff = target.transform.position - transform.position;
            float between = diff.sqrMagnitude;
            if (between < lastDistance)
            {
                _closestShip = target;
                lastDistance = between;
            }
        }
        return _closestShip;
    }
}
