using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xray : MonoBehaviour
{

    public GameObject screenCanvas;
    // Start is called before the first frame update
    void Start()
    {
        screenCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Lugage")) 
        {
            //other.gameObject.SetActive(false);
            screenCanvas.gameObject.SetActive(true);
        }
    }
}
