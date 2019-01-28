using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackUpBombExplosion : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D boxCollider;

    public int explosionRange;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private float explosionDuration;

    [SerializeField]
    private Animator explosionAnimator;

    public GameObject dropper;                  //This is passed by DropBomb.cs

    //Makes bombs collide with the player, once the player isn't on top of the bomb
    //void OnTriggerExit2D(Collider2D other)
    //{
    //    //this.collider2D.isTrigger = false;
    //    boxCollider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
    //    boxCollider.isTrigger = false;
    //}

    //Calls for the explosion
    public void Explode()
    {
        if (FindExplosionRange() < 5 && FindExplosionRange() > 0)
        {
            explosionRange = FindExplosionRange();
        }
        else if (FindExplosionRange() > 5)
        {
            explosionRange = 5;
        }

        //Create explosion on bombLocation
        GameObject explosion = Instantiate(explosionPrefab, this.gameObject.transform.position,
            Quaternion.identity) as GameObject;

        Destroy(explosion, this.explosionDuration);

        if (dropper != null)                                                             //This is to remove errors due to dropper dying
        {
            explosionPrefab.GetComponent<BombExplosionDamage>().dropper = dropper;      //Pass on to BombExplosionDamage.cs
        }

        //Create explosions to all directions
        CreateExplosions(Vector2.left, explosionRange);
        CreateExplosions(Vector2.down, explosionRange);
        CreateExplosions(Vector2.up, explosionRange);
        CreateExplosions(Vector2.right, explosionRange);

        Destroy(this.gameObject);
    }


    //Calculates and draws the explosion sprites accordingly
    private void CreateExplosions(Vector2 direction, int expRange)
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        Vector2 explosionDimensions = explosionPrefab.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 explosionPosition = (Vector2)this.gameObject.transform.position + (explosionDimensions.x * direction);

        for (int explosionCount = 1; explosionCount < expRange; explosionCount++)
        {
            if (dropper != null)
            {
                explosionPrefab.GetComponent<BombExplosionDamage>().dropper = dropper;  //Pass on to BombExplosionDamage.cs
            }
            Collider2D[] colliders = new Collider2D[20];
            Physics2D.OverlapBox(explosionPosition, explosionDimensions * expRange, 0.0f, contactFilter, colliders);   //Checks for colliders in box area
            bool foundObstacleOrWall = false;

            foreach (Collider2D collider in colliders)      //Uses OverLapBox's found colliders
            {
                if (collider)
                {
                    foundObstacleOrWall = (collider.tag == "Wall" || collider.tag == "Obstacle");

                    if (collider.tag == "Obstacle")
                    {
                        //AstarPath.active.UpdateGraphs(collider.bounds);
                        Destroy(collider.gameObject);                               //Destroy obstacle
                        AstarPath.active.Scan();
                        foundObstacleOrWall = true;
                    }
                    if (collider.tag == "Wall")
                    {
                        foundObstacleOrWall = true;
                        break;
                    }
                }
            }
            if (foundObstacleOrWall == true)
            {
                break;
            }
            GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
            Destroy(explosion, this.explosionDuration);
            explosionPosition += (explosionDimensions.x * direction);
        }
    }


    //Get Explosionrange from dropper
    int FindExplosionRange()
    {
        int range;
        if (dropper != null)
        {
            switch (dropper.tag)
            {
                case "Character":
                    range = dropper.GetComponent<PlayerHealth>().explosionRange;
                    break;
                case "Enemy":
                    range = dropper.GetComponent<EnemyHealth>().explosionRange;
                    break;
                default:
                    range = 3;
                    break;
            }
        }
        else
        {
            range = 2;
        }
        return range;
    }
}

