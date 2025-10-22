using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowHazardScript : MonoBehaviour
{
    public bool keyPickedUp;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit a hazard!");
            PlayerFail player = other.GetComponent<PlayerFail>();
            
            if (player != null)
            {
                player.Fail();
            }
            
        }
        // Allows color kills
        if (keyPickedUp)
            {
                Destroy(this.gameObject);
                print("gone");
            }
    }
}
