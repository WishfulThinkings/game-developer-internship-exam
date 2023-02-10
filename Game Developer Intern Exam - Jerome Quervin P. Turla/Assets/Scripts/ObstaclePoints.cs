using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoints : MonoBehaviour
{
    public int points;
    public GameObject mobSpawnerTag;


    private void Start()
    {
        mobSpawnerTag = GameObject.FindGameObjectWithTag("Spawner");      
    }

     public void Die()
    {
        mobSpawnerTag.GetComponent<Spawner>().mobNumber -= 1;      
        Destroy(gameObject);
    }
}
