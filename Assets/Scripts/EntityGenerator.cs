using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <= 0)
        {
            // Generate something
            GameObject randomThing;
            if(Random.Range(0, 101) > 90)
            {
                // Collectible
                randomThing = collectables[Random.Range(0, collectables.Length)];
            }
            else
            {
                // Obstacle
                randomThing = obstacle;
            }

            // Calculate random height
            float randomHeight = Random.Range(0, 5);
            Instantiate(randomThing, new Vector3(player.transform.position.x + 50, randomHeight), Quaternion.identity);

            // Cleanup missed collectables
            GameObject[] generatedCollectables = GameObject.FindGameObjectsWithTag("Collectable").Where((collectable) => collectable.transform.position.x < player.transform.position.x - 10).ToArray();
            for(int i = 0; i < generatedCollectables.Count(); i++)
            {
                Destroy(generatedCollectables[i]);
            }

            // Cleanup missed obstacles
            GameObject[] generatedObstacles = GameObject.FindGameObjectsWithTag("Obstacle").Where((gameObject) => gameObject.transform.position.x < player.transform.position.x - 10).ToArray();
            for (int j = 0; j < generatedObstacles.Count(); j++)
            {
                Destroy(generatedObstacles[j]);
            }

            // Reset cooldown
            cooldown = cooldownMax;

            // CD cant be lower than 1 second, were not making a bullet hell game (yet)
            if(cooldownMax > 1f)
                cooldownMax -= cooldownReduction;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    private float cooldown = 0;
    private float cooldownMax = 3f;
    private float cooldownReduction = 0.05f;

    // Prefabs ////////////////////////////////////////////////////////////////////////////////////
    public GameObject obstacle;
    public GameObject[] collectables;

    // Reference //////////////////////////////////////////////////////////////////////////////////
    public Player player; // Used for relative position
}
