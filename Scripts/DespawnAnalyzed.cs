using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnAnalyzed : MonoBehaviour
{
    public GameManager gameManager;
    private string luggageEtiquette;

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Lugage"))
        {
            // Accédez au composant Luggage de l'objet entrant
            LuggageController luggageController = other.gameObject.GetComponent<LuggageController>();

            luggageEtiquette = luggageController.type.ToString();

            if (luggageController != null && luggageEtiquette == "ANALYZABLE")
            {
                gameManager.AddScore(100);
            }
            else if (luggageController != null && luggageEtiquette == "UNAUTHORIZED")
            {
                gameManager.ReduceLife(1);
            }
            else if (luggageController != null && luggageEtiquette == "AUTHORIZED")
            {
                gameManager.ReduceLife(0.5);
            }
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}