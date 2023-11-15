using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{

    public Material lightMaterial;

    public GameObject gameManager;

    private bool lightIsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkingLightOnRushPeriod());
    }

    IEnumerator BlinkingLightOnRushPeriod()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (gameManager.GetComponent<GameManager>().isRushPeriod)
            {
                if (lightIsOn)
                {
                    lightIsOn = false;
                    lightMaterial.SetColor("_EmissionColor", Color.black);
                } 
                else
                {
                    lightIsOn = true;
                    lightMaterial.SetColor("_EmissionColor", Color.red);
                }

            }
        }
    }
}
