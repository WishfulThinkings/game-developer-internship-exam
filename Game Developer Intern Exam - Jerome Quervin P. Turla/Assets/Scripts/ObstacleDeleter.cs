using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDeleter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ObstaclePoints oP = other.GetComponent<ObstaclePoints>();
        oP.Die();

    }
}
