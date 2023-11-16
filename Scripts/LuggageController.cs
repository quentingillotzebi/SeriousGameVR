using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class LuggageController : MonoBehaviour
{
    public double poids;
    
    public enum luggageType{
        AUTHORIZED,
        UNAUTHORIZED,
        ANALYZABLE
    }
        
    // Start is called before the first frame update
    void Start()
    {
        Random random = new Random();
        poids =  random.NextDouble() * (25 - 10) + 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
