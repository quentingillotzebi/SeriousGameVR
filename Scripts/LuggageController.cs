using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class LuggageController : MonoBehaviour
{
    public double poids;

    public Sprite [] objetsAnalyzable;
    public Sprite [] objetsAuthorized;
    public Sprite [] objetsUnauthorized;

    public Sprite [] objetsValise = new Sprite[5]; 

    public enum LuggageType
    {
        AUTHORIZED,
        UNAUTHORIZED,
        ANALYZABLE
    }

    public LuggageType type; 

    // Start is called before the first frame update
    void Start()
    {
        Random random = new Random();
        poids =  random.NextDouble() * (20 - 10) + 10;
        objetsAnalyzable = Resources.LoadAll<Sprite>("Images/Analyzable");
        objetsAuthorized = Resources.LoadAll<Sprite>("Images/Authorized");
        objetsUnauthorized = Resources.LoadAll<Sprite>("Images/Unauthorized");
        double randomType = random.NextDouble();
        fillValise();
       if (randomType <= 0.2)
       {
           // Ajout de 5 éléments ok
           // Set Tag AUTHORIZED
           type = LuggageType.AUTHORIZED;
       }
       else
       {
           double randomPoidsOuContenu = random.NextDouble();
           if (randomPoidsOuContenu <= 0.4)
           {
               double randomAnalyseOuDanger = random.NextDouble();
               if (randomAnalyseOuDanger <= 0.5)
               {
                   objetsValise[0] = objetsAnalyzable[random.Next(0, objetsAnalyzable.Length)];
                   type = LuggageType.ANALYZABLE;
               }
               else
               {
                   // Valise à analyser en contenu
                   // Set Tag UNAUTHORIZED
                   objetsValise[0] = objetsUnauthorized[random.Next(0, objetsUnauthorized.Length)];
                   type = LuggageType.UNAUTHORIZED;
               }
           }
           else
           {
               // Valise à analyser en poids
               // Set Tag ANALYZABLE
               poids =  random.NextDouble() * (30 - 21) + 21;
               type = LuggageType.ANALYZABLE;
           }
       }
    }


    void fillValise()
    {
        for (int i = 0; i < 5; i++)
        {
            Random rand = new Random();

            objetsValise[i] = objetsAuthorized[rand.Next(0, objetsAuthorized.Length)];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
