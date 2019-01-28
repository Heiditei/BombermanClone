using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public GameObject closestTarget;
    public List<GameObject> targetList;

    [SerializeField]
    private int safeDistance;


    //The actual movement has been changed to A* algorithms, important stuff is to be found in AstarAI.cs 
    //This file only searches for targets anymore.
    void Update()
    {
        MoveToSafety();
        //MoveToTarget(FindClosestTarget(FindTargets()));
        FindClosestTarget(FindTargets());

    }


    //Finds all interactable things
    public List<GameObject> FindTargets()
    {
        List<GameObject> targetList = new List<GameObject>();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy != this.gameObject)
            {
                targetList.Add(enemy);
            }
        }

        foreach(GameObject character in GameObject.FindGameObjectsWithTag("Character"))
        {
            targetList.Add(character);
        }

        foreach (GameObject collectable in GameObject.FindGameObjectsWithTag("Collectable"))
        {
            targetList.Add(collectable);
        }

        return targetList;
    }


    //Calculates the closest interactable thing
    public GameObject FindClosestTarget(List<GameObject> foundTargets)
    {
        closestTarget = null;
        float distance = Mathf.Infinity;
        Vector2 position = transform.position;
        foreach (GameObject target in foundTargets)
        {
            Vector2 diff = (Vector2)target.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < distance)
            {
                closestTarget = target;
                distance = currentDistance;
            }
        }
        GetComponent<EnemyDropBomb>().target = closestTarget;
        GetComponent<Pathfinding.AIDestinationSetter>().target = closestTarget.transform;
        return closestTarget;
    }


    public void MoveToTarget(GameObject target)
    {
        Vector2 targetLocation = target.transform.position;
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, step).normalized;
    }


    public void MoveFromTarget(GameObject target)
    {
        Vector2 targetLocation = target.transform.position;
        float step = - speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, step * Time.deltaTime).normalized;
    }


    public void MoveToSafety()
    {
        Vector2 position = transform.position;
        if (GameObject.FindGameObjectsWithTag("Explosive").Length > 0)
        {
            foreach (GameObject explosion in GameObject.FindGameObjectsWithTag("Explosive"))
            {
                safeDistance = explosion.GetComponent<BombExplosion>().explosionRange;
                Vector2 distance = (Vector2)explosion.transform.position - position;
                if (distance.magnitude < safeDistance)
                {
                    MoveFromTarget(explosion);
                }
            }
        }
    }

}
