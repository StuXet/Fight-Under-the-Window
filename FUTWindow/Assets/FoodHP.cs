using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHP : MonoBehaviour
{


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Picked Up Food");
            Destroy(gameObject);
            // add hp
        }
    }
}
