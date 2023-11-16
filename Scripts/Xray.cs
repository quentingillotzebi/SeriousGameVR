using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    void OnTriggerEnter(Collider v) 
    {
        if (v.gameObject.CompareTag("Lugage")) 
        {
            //other.gameObject.SetActive(false);
            screenCanvas.gameObject.SetActive(true);
            Image[] childrenImages = screenCanvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < 5; i++)
            {
                Debug.Log(v.GetComponent<LuggageController>().objetsValise.Length);
                Sprite s = v.GetComponent<LuggageController>().objetsValise[i];
                childrenImages[i+1].sprite = s;
            }
        }
    }
}
