using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    public int starCount;

    [SerializeField]
    private GameObject starPrefab;

    public List<GameObject> starList = new List<GameObject>();
    // Start is called before the first frame update

    void Start()
    {
        starList.Clear();
        starCount = 0;
        CountStars();
    }

    private void Update()
    {
        if (CountStars() < 1)
        {
            SpawnRand();
        }
        
    }
    public int CountStars()
    {
        starCount = starList.Count;
        return starCount;
        //var stars = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Star");
    }


    public void Spawn(Vector2 location)
    {
        GameObject spawned = Instantiate(starPrefab, location, Quaternion.identity);

        (spawned.GetComponent("Animator") as Animator).enabled = true;
        (spawned.GetComponent("Box Collider 2D") as BoxCollider2D).enabled = true;
        (spawned.GetComponent("Star(Script)") as Star).enabled = true;
        starList.Add(spawned);
        CountStars();
    }


    public void SpawnRand()
    {
        var location = new Vector2(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(-4, 4));
        GameObject spawned = Instantiate(starPrefab, location, Quaternion.identity);

        //(spawned.GetComponent("Animator") as Animator).enabled = true;
        //(spawned.GetComponent("Box> Collider 2D") as BoxCollider2D).enabled = true;
        //(spawned.GetComponent<Star>).enabled = true;

        starList.Add(spawned);
        CountStars();
    }


    public void DestroyStar(GameObject star)
    {
        starList.Remove(star);
        CountStars();
        Destroy(star);
    }
}
