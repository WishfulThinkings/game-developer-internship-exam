using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuilding : MonoBehaviour
{

    void Start()
    {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z); 
    }


}
