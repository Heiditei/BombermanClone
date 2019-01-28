using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropBomb : MonoBehaviour
{

    [SerializeField]
    private GameObject bombPrefab;

    public int explosionRange;              //This is determined in EnemyHealth.cs
    public GameObject target;                   

    private bool withinRange;

    public float dropCooldown;
    float timeFromDrop;

    private void Start()
    {
        //withinRange = false;
        timeFromDrop = 0;
    }


    void Update()
    {   
        if (FindDistance() == true)
        { 
            DropBomb();
            withinRange = false;

        }
       // SmashCrate();
    }

    public void OnTriggerEnter2D(Collider2D inCircle)
    {
        if (inCircle is CircleCollider2D)
        {
            if (inCircle.tag == "Enemy" || inCircle.tag == "Character")
            {
                if(inCircle.gameObject.name != this.gameObject.name)                   //Somehow recognized self as collider
                {
                    DropBomb();
                }

            }
            else if (inCircle.tag == "Obstacle")
            {
                SmashCrate();
                gameObject.GetComponent<EnemyMovement>().MoveFromTarget(inCircle.gameObject);
            }
            else if (inCircle.tag == "Collectable")
            {
                this.gameObject.GetComponent<EnemyMovement>().MoveToTarget(inCircle.gameObject);
            }
        }
    }


    //Summons bombs
    void DropBomb()
    {
        if(Time.time > timeFromDrop)
        {
            timeFromDrop = Time.time + dropCooldown;
            Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity);
            gameObject.GetComponent<EnemyMovement>().MoveToSafety();                //Move away from explosions
            gameObject.GetComponent<EnemyMovement>().MoveFromTarget(bombPrefab);    //Move away from bomb

            //Tell bombExplosion who dropped bomb
            bombPrefab.GetComponent<BombExplosion>().dropper = this.gameObject;
            bombPrefab.GetComponent<BombExplosion>().explosionRange = explosionRange;
            Debug.Log("Bomb dropped by " + this.gameObject);
        }
    }

    //Calculates if target is within explosionRange
    bool FindDistance()
    {
        Vector2 position = transform.position;
        Vector2 distance = (Vector2)target.transform.position - position;

        if (distance.magnitude < explosionRange)
        {
            withinRange = true;
        }
        return withinRange;
    }

       public void SmashCrate()
       {
           Vector2 position = transform.position;
           List<GameObject> obstacleList = new List<GameObject>();
           foreach (GameObject obstacle in GameObject.FindGameObjectsWithTag("Obstacle"))
           {
               obstacleList.Add(obstacle);
               Vector2 distance = (Vector2)obstacle.transform.position - position;
               if(distance.magnitude < explosionRange)
               {
                   DropBomb();
                   gameObject.GetComponent<EnemyMovement>().MoveToSafety();
               }
           }
       }
}