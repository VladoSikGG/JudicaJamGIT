using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInterface : MonoBehaviour
{
    //for movement
    [Header("For Movement")]
    [SerializeField] public float distanceToAttack;
    [SerializeField] public float distanceToSee;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnSpeed;
    //for entaraction
    [Header("For Fight")]
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private float _timeForReload;

    public bool canFire = true;
    [SerializeField]private LineRenderer _lineRender;

    private void Start()
    {
        _lineRender = GetComponent<LineRenderer>();
    }

    public void GoToTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        RotateToTarget(target);
    }

    public void RotateToTarget(Transform target)
    {
        var targetRotation = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, target.position.z) - transform.position, Vector3.up);
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

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distanceToAttack + 1f))
        {
            if (hit.collider.tag == "Player")
            {
               // hit.collider.GetComponent<AllyInterface>().GetDamage(_damage);
                StartCoroutine(Reloading());
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
