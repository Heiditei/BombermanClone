using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerHandler : MonoBehaviour
{
    private bool isSingle;

    [SerializeField]
    private GameObject greenAI;

    // Start is called before the first frame update
    void Start()
    {
        isSingle = GlobalControl.Instance.isSinglePlayer;
        if (isSingle == true)
        {
            GameObject greenNPC = Instantiate(greenAI, transform.position, Quaternion.identity);
            greenNPC.GetComponent<Player2Movement>().enabled = false;
            greenNPC.GetComponent<Player2DropBomb>().enabled = false;
            greenNPC.GetComponent<PlayerHealth>().enabled = false;

            greenNPC.GetComponent<EnemyMovement>().enabled = true;
            greenNPC.GetComponent<EnemyDropBomb>().enabled = true;
            greenNPC.GetComponent<EnemyHealth>().enabled = true;
            greenNPC.GetComponent<Pathfinding.Seeker>().enabled = true;
            greenNPC.GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
            greenNPC.GetComponent<AstarAI>().enabled = true;
            greenNPC.GetComponent<CircleCollider2D>().enabled = true;
            greenNPC.transform.gameObject.tag = "Enemy";

            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<Player2Movement>().enabled = true;
            GetComponent<Player2DropBomb>().enabled = true;
            GetComponent<PlayerHealth>().enabled = true;

            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyDropBomb>().enabled = false;
            GetComponent<EnemyHealth>().enabled = false;
            GetComponent<Pathfinding.Seeker>().enabled = false;
            GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
            GetComponent<AstarAI>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            transform.gameObject.tag = "Character";
        }
    }
}
