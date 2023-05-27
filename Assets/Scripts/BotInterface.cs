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
    //for entaraction
    [Header("For Entaraction")]
    [SerializeField] private int _hp;

    public void GoToTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
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
}
