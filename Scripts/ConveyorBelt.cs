using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorNew : MonoBehaviour
{
    public float speed, materialSpeed;
    Rigidbody rBody;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        rBody.position += Vector3.right * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);

        material.mainTextureOffset += new Vector2(0,-1) * materialSpeed * Time.deltaTime;
    }
}