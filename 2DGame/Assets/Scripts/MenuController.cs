using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{

    public GameObject OptionPanel;
    public GameObject rankPanel;
    public Text rankText;
    public Dropdown shaderDrop;

    private void Start()
    {
        init();
    }

    void init()
    {
        shaderDrop.value = (int) SpriteShader.shaderMode;
    }

    public void newGame()
    {
        GameManager.gameMode = 0;
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        GameManager.gameMode = 1;
        SceneManager.LoadScene(1);
    }

    //選項
    public void OptionBtn()
    {
        OptionPanel.SetActive(!OptionPanel.activeSelf);
    }
    
    //紀錄
    public void RecordBtn()
    {
        rankPanel.SetActive(!rankPanel.activeSelf);
    }

    public void exitGame()
    {
        Application.Quit();
    }


}
