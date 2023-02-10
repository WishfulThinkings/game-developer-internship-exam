using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
public class HoleMovement : MonoBehaviour
{
    [Header("Player Hole Components")]
    public PolygonCollider2D holeMovement;
    public PolygonCollider2D holeGround;
    public MeshCollider GeneratedMeshCollider;
    Mesh generatedMesh;
    public Collider groundCollider;

    private float initialScale = 0.5f;

    

    [Header("Player Movement")]
    public float speed;

    [Header("Scoring System")]
    public int score;
    public int targetScore;
    public int targetScoreMultiplyer;
    
    [Header("Scoring System UI")]
    public GameObject scoreAnimationGameObject;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreAnimText;

    private void Start()
    {
        GameObject[] optimization = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var item in optimization)
        {
            if (item.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(item.GetComponent<Collider>(), GeneratedMeshCollider, true);
            }
        }
    }

    private void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 currentPos = new Vector3(horizontalMove * speed * Time.deltaTime, 0.0f, verticalMove * speed * Time.deltaTime);
        transform.position += currentPos;
        //transform.position = currentPos;
    }
    private void FixedUpdate()
    {
        if(transform.hasChanged == true)
        {
            transform.hasChanged = false;
            holeMovement.transform.position = new Vector2(transform.position.x, transform.position.z);
            holeMovement.transform.localScale = transform.localScale * initialScale;
            MakeHole();
            Make3DMeshCollider();
        }

        if(score >= targetScore)
        {
            LevelUp();
            targetScore *= targetScoreMultiplyer * targetScoreMultiplyer;
        }
    }

    private void MakeHole()
    {
        Vector2[] PointPositions = holeMovement.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = holeMovement.transform.TransformPoint(PointPositions[i]);
        }

        holeGround.pathCount = 2;
        holeGround.SetPath(1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if(generatedMesh != null) { Destroy(generatedMesh); }
        generatedMesh = holeGround.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = generatedMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        ObstaclePoints environmentPoint = other.GetComponent<ObstaclePoints>();
        if(other != null)
        {
           
            scoreAnimationGameObject.SetActive(true);
            //scoreAnimText.text = "+" + environmentPoint.points.ToString();
            
            scoreText.text = score.ToString();

        }

        if(other.gameObject.tag == "BigObstacle" && score < 40)
        {
            Physics.IgnoreCollision(other, groundCollider, false);
            Physics.IgnoreCollision(other, GeneratedMeshCollider, true);
        }
        else if(other.gameObject.tag == "BigObstacle" && score > 40)
        {
            score += environmentPoint.points;
            scoreAnimText.text = "+" + environmentPoint.points.ToString();
            Physics.IgnoreCollision(other, groundCollider, true);
            Physics.IgnoreCollision(other, GeneratedMeshCollider, false);
        }

        else if(other.gameObject.tag == "Obstacle")
        {
            score += environmentPoint.points;
            scoreAnimText.text = "+" + environmentPoint.points.ToString();
            Physics.IgnoreCollision(other, groundCollider, true);
            Physics.IgnoreCollision(other, GeneratedMeshCollider, false);
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, groundCollider, false);
        Physics.IgnoreCollision(other, GeneratedMeshCollider, true);
        scoreAnimText.text = "";
        scoreAnimationGameObject.SetActive(false); 

    }

    void LevelUp()
    {
        initialScale += 0.5f;
        Vector3 tempInitialScale = new Vector3(initialScale, initialScale, initialScale);
        transform.localScale = transform.localScale *= initialScale; 
    }
}
