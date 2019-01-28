using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
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


    public void Explode()
    {
        explosionRange = FindExplosionRange();
        if (explosionRange > 5)
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

    private void CreateExplosions(Vector2 direction, int explRange)
    {
        Vector2 explosionDimensions = explosionPrefab.GetComponent<SpriteRenderer>().bounds.size;
        Vector2 explosionPosition = (Vector2)this.gameObject.transform.position + (explosionDimensions * direction);

        //Ray ray = new Ray(transform.position, direction);
        //RaycastHit2D rayIntersect = Physics2D.GetRayIntersection(ray, explRange, 8);
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, direction, explRange, 8);
        Debug.DrawRay(transform.position, direction * explRange, Color.green, 0.5f);
        bool unpassableObstacle = false;

        for(int i = 1; i < explRange; i++)
        {
            RaycastHit2D rayLoopHit = Physics2D.Raycast(explosionPosition, direction, explRange);
            Debug.DrawRay(explosionPosition, direction * explRange, Color.red, 0.5f);
            Debug.Log("1 "+direction);

            if (rayLoopHit.collider != null)
            {
                Debug.Log("2 "+direction);
                if (rayLoopHit.collider.tag == "Wall" || rayLoopHit.collider.tag == "Obstacle")
                {
                    Debug.Log("3");
                    if(rayLoopHit.collider.tag == "Obstacle")
                    {
                        Debug.Log("4");
                        Destroy(rayLoopHit.collider.gameObject);                    //Destroy obstacle
                        AstarPath.active.Scan();
                    }
                    unpassableObstacle = true;
                    Debug.Log("5");
                    break;
                }
                Debug.Log("6");
            }
            Debug.Log("7");
            if (unpassableObstacle == false)
            {
                Debug.Log("8");
                GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
                Destroy(explosion, this.explosionDuration);
                explosionPosition += (explosionDimensions * direction);
            }
            Debug.Log("9");
        }
        Debug.Log("10");
        //for (int i = 1; i < explRange; i++)
        //{
        //    
        //    {
        //        if (hit.collider.tag == "Obstacle")
        //        {
        //            //AstarPath.active.UpdateGraphs(collider.bounds);
        //            Destroy(hit.collider.gameObject);                    //Destroy obstacle
        //            AstarPath.active.Scan();
        //            unpassableObstacle = true;
        //        }
        //        if (hit.collider.tag == "Wall")
        //        {
        //            unpassableObstacle = true;
        //        }
        //    }
        //    if (unpassableObstacle == true)
        //    {
        //        break;
        //    }
        //    GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
        //    Destroy(explosion, this.explosionDuration);
        //    explosionPosition += (explosionDimensions * direction);
        //}
        //Instanssioi räjähdyksen sijaintiin a
        //määrittää a:n sijainnin a+1
        //a < explRange

        //Löytää colliderit, jos .tag = seinä, break 
        //Jos .tag Obstacle, tuhoa se ja break.
    }
    //for (int explosionCount = 1; explosionCount < explRange; explosionCount++)
    //{
    //    if (hit.collider.tag == "Wall" || hit.collider.tag == "Obstacle")
    //    { 
    //        if (hit.collider.tag == "Obstacle")
    //        {
    //            //AstarPath.active.UpdateGraphs(collider.bounds);
    //            Destroy(hit.collider.gameObject);                    //Destroy obstacle
    //            AstarPath.active.Scan();
    //            unpassableObstacle = true;
    //        }
    //        if (hit.collider.tag == "Wall")
    //        {
    //            unpassableObstacle = true;
    //        }
    //    }
    //    if(unpassableObstacle = false)
    //    {
    //        GameObject explosion = Instantiate(explosionPrefab, explosionPosition, Quaternion.identity) as GameObject;
    //        Destroy(explosion, this.explosionDuration);
    //        explosionPosition += (explosionDimensions.x * direction);
    //    }
    //}
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