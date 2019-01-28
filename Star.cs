using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Star : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    private GameObject picker;

    public GameObject starSpawn;

    void Start()
    {
        // StarSpawn.GetComponent<StarHandler>().starList.Add(this.gameObject);
        starSpawn = GameObject.Find("StarSpawn");
        starSpawn.GetComponent<StarHandler>().CountStars();
    }

    //
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other is BoxCollider2D)
        {
            picker = other.gameObject;

            System.Random rnd = new System.Random();
            int chance = rnd.Next(1, 4);
            Debug.Log("Star: " + chance);

            switch (picker.tag)
            {
                //Player
                case "Character":
                    switch (chance)
                    {
                        case 1:         //Gain life
                            picker.GetComponent<PlayerHealth>().GainLife();
                            Debug.Log(picker.name + " Gained life");
                            break;
                        case 2:         //Get points
                            picker.GetComponent<PlayerScore>().SetPoints(50);
                            Debug.Log(picker.name + " Gained points");
                            break;
                        case 3:         //Explosionrange+1
                            if (picker.GetComponent<PlayerHealth>().explosionRange < 5)
                            { 
                                picker.GetComponent<PlayerHealth>().explosionRange++;
                                Debug.Log(picker.name + " explosionRange increased");
                            }
                            break;
                        case 4:         //Explosionrange-1
                            if (picker.GetComponent<PlayerHealth>().explosionRange > 1)
                            {
                                picker.GetComponent<PlayerHealth>().explosionRange--;
                                Debug.Log(picker.name + " explosionRange decreased");
                            }
                            break;
                        case 5:         //Explode
                            GetComponent<BombExplosion>().Explode();
                            GetComponent<BombExplosion>().explosionRange = 3;
                            GetComponent<BombExplosion>().dropper = this.gameObject;
                            Debug.Log("Star exploded");
                            break;
                    }
                    break;


                //Enemy
                case "Enemy":
                    switch (chance)
                    {
                        case 1:         //Gain life
                            picker.GetComponent<EnemyHealth>().GainLife();
                            Debug.Log(picker.name + " Gained life");
                            break;
                        case 2:         //Get points
                            picker.GetComponent<PlayerScore>().SetPoints(50);
                            Debug.Log(picker.name + " Gained points");
                            break;

                        case 3:         //Explosionrange+1
                            if (picker.GetComponent<EnemyHealth>().explosionRange < 5)
                            {
                                picker.GetComponent<EnemyHealth>().explosionRange++;
                                Debug.Log(picker.name + " explosionRange increased");
                            }
                            break;
                        case 4:         //Explosionrange-1
                            if (picker.GetComponent<EnemyHealth>().explosionRange > 1)
                            {
                                picker.GetComponent<EnemyHealth>().explosionRange--;
                                Debug.Log(picker.name + " explosionRange decreased");
                            }
                            break;
                        case 5:         //Explode
                            GetComponent<BombExplosion>().Explode();
                            GetComponent<BombExplosion>().explosionRange = 3;
                            GetComponent<BombExplosion>().dropper = this.gameObject;
                            Debug.Log("Star exploded");
                            break;
                    }
                    break;

                default:
                    starSpawn.GetComponent<StarHandler>().DestroyStar(this.gameObject);
                    break;
            }

        //Spawn a new one
        //Instantiate(starPrefab, this.gameObject.transform.position, Quaternion.identity);

            if (CountProxy() < 1)
            {
                starSpawn.GetComponent<StarHandler>().SpawnRand();
            }
            DestroyStar();
        }
              }

    public int CountProxy()
    {
        return starSpawn.GetComponent<StarHandler>().CountStars();
    }

    public void DestroyStar()
    {
        starSpawn.GetComponent<StarHandler>().starList.Remove(this.gameObject);
        starSpawn.GetComponent<StarHandler>().CountStars();
        Destroy(this.gameObject);
    }
}
