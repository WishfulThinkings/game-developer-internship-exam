using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private float moveSpeed = 10f;
    private Vector3 randomPosition;

    //AI movement
    public float minX,
    maxX,
    minZ,
    maxZ;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        randomPosition = new Vector3(Random.Range(minX, maxX), 0.0f,Random.Range(minZ, maxZ));
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomPosition, moveSpeed * Time.deltaTime);
    }
}
