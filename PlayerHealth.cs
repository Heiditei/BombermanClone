using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int numberOfLives;                  //number of lives

    [SerializeField]
    private float invulnerabilityDuration;      //How long player will be invulnerable after hit

    private bool isInvulnerable = false;        //Is player invulnerable?

    [SerializeField]
    private GameObject playerLifeImage;         //Heart symbols

    private List<GameObject> lifeImages;        //List of player's heart symbols

    public int explosionRange;                  //How large explosions player bombs make

    [SerializeField]
    private GameObject playerLifeGrid;          //This player's grid to show health

    void Start()
    {
        //Finds the grid player hearts are stored on, then creates hearts to match amount of player lives.
        this.lifeImages = new List<GameObject>();
        for (int lifeIndex = 0; lifeIndex < this.numberOfLives; ++lifeIndex)
        {
            GameObject lifeImage = Instantiate(playerLifeImage, playerLifeGrid.transform) as GameObject;
            this.lifeImages.Add(lifeImage);
        }
    }

    void Update()
    {
        switch (gameObject.name)
        {
            case "Blue":
                GetComponent<PlayerDropBomb>().explosionRange = explosionRange;             //Passed to playerDropBomb
                break;
            case "Green":
                GetComponent<Player2DropBomb>().explosionRange = explosionRange;             //Passed to playerDropBomb
                break;
        }

    }

    //Manages what happens when player gets hit
    public void LoseLife()
    {
        if (!this.isInvulnerable)
        {
            if (this.numberOfLives > 0)                                             //If player has lives left
            { 
                this.numberOfLives--;                                               //Lives -1
                GameObject lifeImage = this.lifeImages[this.lifeImages.Count - 1];  //Finds last heart in row
                Destroy(lifeImage);                                                 //Removes said heart image
                this.lifeImages.RemoveAt(this.lifeImages.Count - 1);                //Removes life from list
            }
            if (this.numberOfLives <= 0)                                            //If player has no life left
            {
                gameObject.GetComponent<PlayerScore>().SetPoints(-100);             //Give minuspoints
                GetComponent<PlayerScore>().SavePlayer();                           //Save points
                Destroy(this.gameObject);                                           //Remove player
            }
            this.isInvulnerable = true;                                             //Make player invulnerable for a moment
            Invoke("BecomeVulnerable", this.invulnerabilityDuration);
        }
    }

    public void GainLife()
    {
        this.numberOfLives++;                                                       //Lives +1
        GameObject lifeImage = Instantiate(playerLifeImage, playerLifeGrid.transform) as GameObject;    //Creates a heart
        this.lifeImages.Add(lifeImage);                                         
    }

    private void BecomeVulnerable()
    {
        this.isInvulnerable = false;
	}
}
