using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------------------------------------------------------------

    public void ShowEndGamePanel(int score)
    {
        endgameText.text = string.Format("You survived for {0} seconds and earned {1} points!", Time.timeSinceLevelLoad.ToString("F2"), score);
        panel.SetActive(true);
    }

    // Button Actions /////////////////////////////////////////////////////////////////////////////

    public void NewGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    //---------------------------------------------------------------------------------------------

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    public GameObject panel;
    public Text endgameText;
}
