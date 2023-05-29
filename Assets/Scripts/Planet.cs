using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject canvas;
    public bool exPlanet;
    public bool canMine;
    public bool canCheck;
    [SerializeField] public int _price;
    [SerializeField] private Material _bought;
    [SerializeField] private Vector3 _player;

    private void Start()
    {
        cam = Camera.main;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        _price = Random.Range(1, 100);
    }

    private void CheckPlayerNear()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Player")
            {
                Debug.Log("tue;");
                canCheck = true;
                break;
            }
            else
            {
                Debug.Log("tue;2");
                canCheck = false;
            }
        }
    }
    void Update()
    {
        CheckPlayerNear();
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
                    if (hit.collider.tag == "Planet" && !exPlanet && canCheck)
                    {
                        canvas.GetComponent<UIcontroller>().checkPlanet = true;
                        canvas.GetComponent<UIcontroller>().price = _price;
                        canvas.GetComponent<UIcontroller>().currentPlanet = this.gameObject.GetComponent<Planet>();
                    }
                        
                }
            }
    }

    public void BuyPlanet()
    {
        exPlanet = true;
        canMine = true;
        this.gameObject.GetComponent<MeshRenderer>().material = _bought;
        this.gameObject.GetComponent<AudioSource>().Play();
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
