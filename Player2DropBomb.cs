using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DropBomb : MonoBehaviour
{

    [SerializeField]
    private GameObject bombPrefab;

    [SerializeField]
    private int maxExplosives;

    public int explosionRange;
    public int activeExplosives;

    // Update is called once per frame
    void Update()
    {
        //If button is pressed, dropping bombs is called.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DropBomb();
        }
    }

    //Summons bombs
    void DropBomb()
    {
        if (activeExplosives < maxExplosives)
        {
            Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity);
            //Tell bombExplosion who dropped bomb
            bombPrefab.GetComponent<BombExplosion>().dropper = this.gameObject;
            bombPrefab.GetComponent<BombExplosion>().explosionRange = explosionRange;
            Debug.Log("Bomb dropped by " + this.gameObject);
        }
    }
}