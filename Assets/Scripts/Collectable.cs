using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        asrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public void Collect(Player player)
    {
        player.points += points;
        switch (type)
        {
            case 1:
                // Coins only give points
                break;
            case 2:
                // Add 1 health (potentially infinite, could limit it but its ok)
                player.lifes++;
                break;
            case 3:
                // Invulnerable
                player.CloverPickup();
                break;
            case 4:
                // Speed
                player.PotionPickup();
                break;
        }
        Destroy(gameObject);
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public int points;
    public int type; // 1- coin 2- gem 3-clover 4-potion

    // Audio clips ////////////////////////////////////////////////////////////////////////////////
    AudioSource asrc;
    public AudioClip audioClip;
}
