using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    GameObject player;
    public int healthBoost = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "FoodGrab")
        {
            Debug.Log("Picked Up Food");
            player.GetComponent<PlayerCombat>().currentHP += healthBoost;
            Destroy(gameObject);
            // add hp
        }
    }



}
