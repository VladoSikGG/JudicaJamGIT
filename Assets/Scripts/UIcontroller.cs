using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : Planet
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject exPlanett;
    [SerializeField] private GameObject planet;
    [SerializeField] private Text textCristals;
    [SerializeField] private Text textPrice;
    [HideInInspector] public bool checkPlanet;
    [HideInInspector] public float crystals;
    private int price;

    void Start()
    {
        // AddPlanets();//adding planets
        planet = this.gameObject;
        panel.SetActive(false);
        price =  Random.Range(1, 12);
    }

    void Update()
    {
        if (checkPlanet)
        {
            textPrice.text = "Cristals need to explore: " + price;
            panel.SetActive(true);
        }
        else if (!checkPlanet)
            panel.SetActive(false);

        textCristals.text = "Cristals: " + crystals;
    }

    public void ExploreButton()
    {
        if (price <= crystals)
        {
            crystals -= price;
            GameObject ExPlanet = Instantiate(exPlanett, planet.transform.position, Quaternion.identity); //replacing an old planet with a new one
            checkPlanet = false;
            ExPlanet.GetComponent<Planet>().BuyPlanet();
        }
    }

    public void SkipButton()
    {
        checkPlanet = false;
    }

    private void AddPlanets() //adding planets
    {
        for (int i = 0; i < 3; i = i++) 
        {
            Instantiate(planet, new Vector3(Random.Range(0, 120), 0, Random.Range(0, 120)), Quaternion.identity);
        }
    }
}
