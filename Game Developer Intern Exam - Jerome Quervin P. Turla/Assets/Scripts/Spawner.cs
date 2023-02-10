using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public HoleMovement playerHole;
    public GameObject[] enemies;
    [Header("Spawner Attributes")]
    public float spawnTime;
    public int mobLimitNumber;
    public float mobNumber;
    [SerializeField]
    private bool mobLimit;
    
    [SerializeField]
    private int minSpawnRange = 10, maxSpawnRange = 10;
    void Start()
    {
        StartCoroutine(EnemySpawner(spawnTime));
    }

    private void Update()
    {
        if(mobNumber == mobLimitNumber)
        {
            StopCoroutine(EnemySpawner(spawnTime));
            mobLimit = true; 
        }

        else if(mobNumber < mobLimitNumber && mobLimit == true)
        {
            Invoke("Spawn", spawnTime);
            
        }
        
        if (playerHole.score == playerHole.targetScore)
        {
            maxSpawnRange = maxSpawnRange + 10;
        }

    }
    IEnumerator EnemySpawner(float spawnRate)
    {
        while(mobNumber < mobLimitNumber)
        {
            yield return new WaitForSeconds(spawnRate);
            Spawn();
            //StartCoroutine(EnemySpawner(spawnRate));         
        }
      

    }

    public void Spawn()
    {
        Instantiate(enemies[Random.Range(0, enemies.Length - 1)], new Vector3(Random.Range(-minSpawnRange, maxSpawnRange), 5f, Random.Range(-minSpawnRange, maxSpawnRange)), Quaternion.identity);
        mobNumber++;
        CancelInvoke("Spawn");

    }
}
