using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public int leng;
    public bool hasBorder = true;
    public Text msgText;
    public Text scoreText;
    public Text lengText;
    public Image bg;
    public Image pauseImg;
    public Sprite[] pauseSprites;

    private float tm;
    private Color tmp;

    [HideInInspector]public bool isPause = false;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
         tm = Time.timeScale;
        if(PlayerPrefs.GetInt("border",1) == 0)
        {
            hasBorder = false; 

        }

    }

    private void Update()
    {
        switch (score)
        {
            case 50:
                ColorUtility.TryParseHtmlString("#CCEEFFFF", out tmp);
                bg.color = tmp;
                msgText.text = "阶段" + 2;
                break;
            case 100:
                ColorUtility.TryParseHtmlString("#CCEEDBFF", out tmp);
                bg.color = tmp;
                msgText.text = "阶段" + 2;
                break;
            case 200:
                ColorUtility.TryParseHtmlString("#EBFFCCFF", out tmp);
                bg.color = tmp;
                msgText.text = "阶段" + 3;
                break;
            case 300:
                ColorUtility.TryParseHtmlString("#FFF#CCFF", out tmp);
                bg.color = tmp;
                msgText.text = "阶段" + 4;
                break; 
            case 400:
                ColorUtility.TryParseHtmlString("#FFDACCFF", out tmp);
                bg.color = tmp;
                msgText.text = "无尽模式";
                break;
        }
    }

    // Update is called once per frame
    public  void UpdateUI(int s = 5)
    {
        score += s;
        leng += 1;
        scoreText.text = "得分: \n" + score;
        lengText.text = "长度: \n" + leng;
    }

    public void Pause()
    {
        isPause = !isPause;
        if(isPause)
        {
            Time.timeScale = 0;
            pauseImg.sprite = pauseSprites[0];
        }
        else
        {
            Time.timeScale = tm;
            pauseImg.sprite = pauseSprites[1];
        }
    }

    public void ChangeToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void ChangeToMain()
    {

        SceneManager.LoadScene(1);
    }
}

