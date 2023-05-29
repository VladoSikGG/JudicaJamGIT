using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject canvas;
    public bool exPlanet;
    public bool canMine;

    private void Start()
    {
        cam = Camera.main;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    void Update()
    {
        CheckRay();

        if (exPlanet && canMine)
        {
            StartCoroutine(MinePlanet());
        }
    }

    void CheckRay()
    {
            if (Input.GetMouseButtonDown(0) )
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Planet" && !exPlanet)
                        canvas.GetComponent<UIcontroller>().checkPlanet = true;
                }
            }
    }

    public void BuyPlanet()
    {
        exPlanet = true;
        canMine = true;
        Destroy(this.gameObject);
    }
    public IEnumerator MinePlanet() //coroutine to replenish crystals
    {
        canMine = false;
        canvas.GetComponent<UIcontroller>().crystals++;
        Debug.Log("Cistals");
        yield return new WaitForSeconds(2f);
        canMine = true;
    }
}
