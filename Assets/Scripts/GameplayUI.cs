using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update score
        scoreboard.text = Mathf.Floor(player.points).ToString();

        // If player life has changed, update life counter
        if(currentLifeShown != player.lifes)
        {
            // Update counter helper
            currentLifeShown = player.lifes;

            // Remove previous icons
            for(int i = 0; i<lifePanel.transform.childCount; i++)
            {
                Destroy(lifePanel.transform.GetChild(i).gameObject);
            }

            // Instantiate life prefab in the panel
            for(int j = 0; j< currentLifeShown; j++)
            {
                Instantiate(lifeIconPrefab, lifePanel.transform);
            }
        }
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public GameObject lifePanel;
    public GameObject lifeIconPrefab;
    public Player player;
    public Text scoreboard;

    private int currentLifeShown = 0;
}
