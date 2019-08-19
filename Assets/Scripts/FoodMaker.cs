using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMaker : MonoBehaviour
{
    public GameObject food;
    public GameObject reward;
    public Sprite[] foodSprite;
    public Image foodImg;

    private static FoodMaker _instance;
    public static FoodMaker Instance
    {
        get { return _instance; }
    }
    private int y = 10;
    private int minX = -15;
    private int maxX = 17;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        foodImg = food.GetComponent<Image>();
        FoodMake(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoodMake(bool isReward)
    {
        int index = Random.Range(0,foodSprite.Length);
        int moveX = Random.Range(minX,maxX);
        int moveY = Random.Range(-y,y);

        foodImg.sprite = foodSprite[index];
        GameObject go = Instantiate(food);
        go.transform.SetParent(this.transform,false);
        go.transform.localPosition = new Vector3(moveX*30, moveY*30,0);
        if(isReward)
        {
            GameObject re = Instantiate(reward);
            re.transform.SetParent(this.transform, false);
            moveX = Random.Range(minX, maxX);
            moveY = Random.Range(-y, y);
            re.transform.localPosition = new Vector3(moveX * 30, moveY * 30, 0);

        }
    }
}
