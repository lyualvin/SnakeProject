using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    public Text lastText;
    public Text bestText;

    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle noBorder;
    // Start is called before the first frame update
    void Start()
    {
        lastText.text = "上次：长度" + PlayerPrefs.GetInt("lastl",0) + "得分："+ PlayerPrefs.GetInt("lasts",0);
        bestText.text = "最好：长度" + PlayerPrefs.GetInt("bestl", 0) + "得分：" + PlayerPrefs.GetInt("bests", 0);
        if(PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102"); 
        }
        else
        {
            yellow.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202"); 
        }

        if (PlayerPrefs.GetInt("border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("border", 1);
        }
        else
        {
            noBorder.isOn = true;
            PlayerPrefs.SetInt("border", 0);
        }
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetString("sh")+"-"+ PlayerPrefs.GetString("sb01") +"-" +PlayerPrefs.GetString("sb02"));
    }
    public void BlueSelect(bool isOn)
    {
        if(isOn)
        {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102"); 
        }
    }
    public void YellowSelect(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202"); 
        }
    }
    public void BorderSelect(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Border", 1);
        }
    }
    public void NoBorderSelect(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Border", 0);

        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
