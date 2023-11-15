using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float period = 7.0f;
    public GameObject LugagePrefab;
    
    IEnumerator WaitAndChangeType()
    {
        while(true) {
            yield return new WaitForSeconds(period);
            GameObject LugageInstance = Instantiate(LugagePrefab, transform.position, transform.rotation *  Quaternion.Euler(0, 90, 90));
            LugageInstance.gameObject.tag = "Lugage";
        }
    }
    void Start()
    {
        StartCoroutine(WaitAndChangeType());
    }
}
