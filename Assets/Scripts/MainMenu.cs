using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreboard.text = string.Format("High-Score: {0}", highscore.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Button actions /////////////////////////////////////////////////////////////////////////////

    public void NewGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    //---------------------------------------------------------------------------------------------

    public void ExitGame()
    {
        Application.Quit();
    }

    // Data ///////////////////////////////////////////////////////////////////////////////////////
    private int highscore;

    public Text scoreboard;
}
