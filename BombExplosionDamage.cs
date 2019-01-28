using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosionDamage : MonoBehaviour {

    public GameObject dropper;                                                          //This is passed by BombExplosion.cs
    private GameObject target = null;
    StarHandler starHandler;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider is BoxCollider2D)
        {
            GameObject starSpawn = GameObject.Find("StarSpawn");                        //This is to reference to StarSpawn
            if (starSpawn != null)
            {
                starHandler = starSpawn.GetComponent<StarHandler>();                    //Otherwise gives out error
            }

            target = collider.gameObject;                                               //Shortens code a bit

//          if (collider.tag == "Character" || collider.tag == "Enemy")                 //Checks if hits a character or enemy
            switch (target.tag)
            {

                case "Character":                      
                    if (GameObject.ReferenceEquals(target, dropper))                    //if dropper == self
                    {
                        target.GetComponent<PlayerScore>().SetPoints(-1000);            //Character loses tons of points for suicide
                    }
                    if(dropper != null)
                    {
                        dropper.GetComponent<PlayerScore>().SetPoints(+500);                //Dropper gains points for kill
                    }
                    target.GetComponent<PlayerHealth>().LoseLife();                     //Character life -1
                    break;


                case "Enemy":
                    if (GameObject.ReferenceEquals(target, dropper))                    //if dropper == self
                    {
                        target.GetComponent<PlayerScore>().SetPoints(-1000);            //minuspoints for suicide
                    }
                    if(dropper != null)
                    {
                        dropper.GetComponent<PlayerScore>().SetPoints(+500);                //points for kill
                    }
                    target.GetComponent<EnemyHealth>().LoseLife();                      //target life -1
                    break;

                case "Explosive":
                    target.GetComponent<BombExplosion>().Explode();         //Explodes other explosions
                    break;


                case "Collectables":                                        // Destroy collectable
                    //if (target.GetComponent<Star>() != null)
                    if (target.GetComponent<Star>().CountProxy() < 1)
                    {
                        starHandler.SpawnRand();                            //Spawn to random location
                    }
                    starHandler.DestroyStar(target);                        //Destroy target Star
                    break;
            }
        }
    }
}
