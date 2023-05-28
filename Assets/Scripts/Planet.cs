using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject canvas;
    public bool exPlanet;


    void Start()
    {
        if (exPlanet)
            StartCoroutine(MinePlanet());
    }

    void Update()
    {
        CheckRay();
    }

    void CheckRay()
    {
        if (!exPlanet)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Planet")
                        canvas.GetComponent<UIcontroller>().checkPlanet = true;
                }
            }
        }

    }

    IEnumerator MinePlanet() //coroutine to replenish crystals
    {
        while (true)
        {
            canvas.GetComponent<UIcontroller>().crystals++;
            Debug.Log("Cistals");

            yield return new WaitForSeconds(60f);
        }

    }
}
