using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ships : MonoBehaviour
{
    public Camera cam;
    private NavMeshAgent agent;
    public bool canMove;


    void Start()
    {
        SelectObjects.unit.Add(gameObject); // ���������� �������� � ������ ���� ������, ������� �� ����� ��������
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (canMove)
            MoveObject();
    }

    void MoveObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); //��������� ��������� ����� ���
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                agent.SetDestination(hit.point); //�������� �������
        }
    }

}
