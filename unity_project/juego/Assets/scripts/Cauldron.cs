using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour {

    public bool enableLiquid;
    public bool enableFire;
    public bool enableSmoke;
    public bool enableGood;

    public Material emptyMaterial;
    public Material fullMaterial;
    public Material goodMaterial;

    private bool prevEnableLiquid;
    private bool prevEnableFire;
    private bool prevEnableSmoke;
    private bool prevEnableGood;

    // Use this for initialization
    void Start () {

        liquid(enableLiquid);
        fire(enableFire);
        smoke(enableSmoke);
        good(enableGood);

        prevEnableLiquid = enableLiquid;
        prevEnableFire = enableFire;
        prevEnableSmoke = enableSmoke;
        prevEnableGood = enableGood;

	}
	
	// Update is called once per frame
	void Update () {

        if (prevEnableLiquid != enableLiquid)
        {
            liquid(enableLiquid);
            prevEnableLiquid = enableLiquid;
        }

        if (prevEnableFire != enableFire)
        {
            fire(enableFire);
            prevEnableFire = enableFire;
        }

        if(prevEnableSmoke != enableSmoke)
        {
            smoke(enableSmoke);
            prevEnableSmoke = enableSmoke;
        }

        if(prevEnableGood != enableGood)
        {
            good(enableGood);
            prevEnableGood = enableGood;
        }

	}

    private void liquid(bool enable)
    {
        Renderer rend = transform.Find("liquid/default").gameObject.GetComponent<Renderer>();
        if (enable) rend.material = enableGood ? goodMaterial : fullMaterial;
        else        rend.material = emptyMaterial;
    }

    private void fire(bool enable)
    {
        transform.Find("fire").gameObject.SetActive(enable);
    }

    private void smoke(bool enable)
    {
        GameObject smokeEnable = enableGood ?
            transform.Find("good_smoke").gameObject :
            transform.Find("smoke").gameObject;

        GameObject smokeDisable = !enableGood ?
            transform.Find("good_smoke").gameObject :
            transform.Find("smoke").gameObject;

        smokeEnable.SetActive(enable);
        smokeDisable.SetActive(false);
    }

    private void good(bool enable)
    {
        Renderer rend = transform.Find("liquid/default").gameObject.GetComponent<Renderer>();
        if (enableLiquid) rend.material = enableGood ? goodMaterial : fullMaterial;
        else              rend.material = emptyMaterial;

        GameObject smokeEnable = enableGood ?
            transform.Find("good_smoke").gameObject :
            transform.Find("smoke").gameObject;

        GameObject smokeDisable = !enableGood ?
            transform.Find("good_smoke").gameObject :
            transform.Find("smoke").gameObject;

        smokeEnable.SetActive(enableSmoke);
        smokeDisable.SetActive(false);
    }

}
