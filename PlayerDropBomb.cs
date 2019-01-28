using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropBomb : MonoBehaviour {

    [SerializeField]
    private GameObject bombPrefab;

    public int explosionRange;
    public int activeExplosives;

    
    // Update is called once per frame
    void Update()
    {
    //If button is pressed, dropping bombs is called.
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            DropBomb();
        }
    }

    //Summons bombs
    void DropBomb()
    {
            Instantiate(bombPrefab, this.gameObject.transform.position, Quaternion.identity);
            //Instantiate(bombPrefab, new Vector2(Mathf.Round(this.gameObject.transform.position.x),
            //                                    Mathf.Round(this.gameObject.transform.position.y)), Quaternion.identity);

            //Tell bombExplosion who dropped bomb
            bombPrefab.GetComponent<BombExplosion>().dropper = this.gameObject;
            bombPrefab.GetComponent<BombExplosion>().explosionRange = explosionRange;
            Debug.Log("Bomb dropped by " + this.gameObject);
    }
}