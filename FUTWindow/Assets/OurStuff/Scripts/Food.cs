using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    PlayerCombat player;
    public int healthBoost = 10;

    void start()
    {
        GameObject.FindGameObjectWithTag("Player");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Picked Up Food");
            player.currentHP = +healthBoost;
            Destroy(gameObject);
            // add hp
        }
    }
}
