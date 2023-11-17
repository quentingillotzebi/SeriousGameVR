using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = System.Random;



public class Xray : MonoBehaviour
{

    public GameObject screenCanvas;
	public GameObject screenPoidsCanvas;
    // Start is called before the first frame update
    void Start()
    {
        screenCanvas.gameObject.SetActive(false);
		screenPoidsCanvas.gameObject.SetActive(false);
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
                Sprite s = v.GetComponent<LuggageController>().objetsValise[i];
                childrenImages[i+1].sprite = s;
				Random rand = new Random();
				int angleRotate = rand.Next(0,270);
				childrenImages[i+1].transform.Rotate(Vector3.forward * angleRotate);
            }
			screenPoidsCanvas.gameObject.SetActive(true);
			TextMeshProUGUI textPoids = screenPoidsCanvas.GetComponentsInChildren<TextMeshProUGUI>()[0];
			textPoids.text = v.GetComponent<LuggageController>().poids.ToString("0.00") + " kgs";
			
        }
    }
}
