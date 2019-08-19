using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SnakeHead : MonoBehaviour
{

    public float velocity = 0.35f;
    public float step; 
    public GameObject boom; 
    public List<Transform> bodyList = new List<Transform>();
    public GameObject bodyPerfab; 
    public Sprite[] bodySprites = new Sprite[2]; 


    private Transform canvas;
    private bool isDie = false;
    private float x ;
    private float y;
    private RectTransform shTransform;
    private Vector3 headPos;
    // Start is called before the first frame update
    // up10  down10 left15 rigth17

    void Start()
    {
        canvas = GameObject.Find("Canvas").transform;


        gameObject.GetComponent<Image>().sprite =  Resources.Load<Sprite>(PlayerPrefs.GetString("sh"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02"));

        Debug.Log(PlayerPrefs.GetString("sh") + PlayerPrefs.GetString("sb01") + PlayerPrefs.GetString("sb02"));

        shTransform = GetComponent<RectTransform>(); 
        InvokeRepeating("Move", 0, velocity);
        x = 0;
        y = step;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& GameManager.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity - 0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && GameManager.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W) && y!= -step && GameManager.Instance.isPause == false && isDie == false)
        {
            shTransform.localRotation = Quaternion.Euler(0,0,0);
            x = 0;
            y = step;
        }
        else if (Input.GetKey(KeyCode.S) && y != step && GameManager.Instance.isPause == false && isDie == false)
        {
            shTransform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        else if (Input.GetKey(KeyCode.A) && x != step && GameManager.Instance.isPause == false && isDie == false)
        {
            shTransform.localRotation = Quaternion.Euler(0, 0,  90);
            x = -step;
            y = 0;
        }
        else if (Input.GetKey(KeyCode.D) && x != -step && GameManager.Instance.isPause == false && isDie == false)
        {
            shTransform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }

    }

    void Move()
    {
        headPos = shTransform.localPosition;
        shTransform.localPosition = new Vector3(headPos.x+x, headPos.y+y, headPos.z);

        if(bodyList.Count > 0)
        {
            /*
            bodyList.Last().localPosition = headPos;
            bodyList.Insert(0, bodyList.Last());
            bodyList.RemoveAt(bodyList.Count - 1);
            */
            for (int i = bodyList.Count-2; i >= 0;  i -- )
            {
                bodyList[i + 1].localPosition = bodyList[i].localPosition;

            }
            bodyList[0].localPosition = headPos;

        }
    }

    void Die()
    {
        CancelInvoke();
        isDie = true;
        Instantiate(boom);
        PlayerPrefs.SetInt("lastl", GameManager.Instance.leng);
        PlayerPrefs.SetInt("lasts",GameManager.Instance.score);
        if(PlayerPrefs.GetInt("bests", 0) < GameManager.Instance.score)
        {
            PlayerPrefs.SetInt("bestl", GameManager.Instance.leng);
            PlayerPrefs.SetInt("bests", GameManager.Instance.score);
        }
        StartCoroutine(GameOver(2f));
    }

    IEnumerator GameOver(float t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(1);
    }

    void Grow()
    {
        int index = (bodyList.Count %2 ==0)?0:1;
        GameObject go = Instantiate(bodyPerfab, new Vector3(150,400,0), Quaternion.identity);
        go.GetComponent<Image>().sprite = bodySprites[index];
        go.transform.SetParent(canvas, false);
        bodyList.Add(go.transform); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            Destroy(collision.gameObject);
            Grow();
            FoodMaker.Instance.FoodMake((Random.Range(0,100) < 20) ? true : false);
            GameManager.Instance.UpdateUI(5);
        }
        else if (collision.tag == "Reward")
        {
            Destroy(collision.gameObject);
            Grow();
            GameManager.Instance.UpdateUI(Random.Range(20,100));
        }
        else if(collision.tag == "Body")
        {
            Die();

        }
        else
        {
            switch (collision.gameObject.name)
            {
                case "up":
                    transform.localPosition = new Vector3(transform.localPosition.x,- transform.localPosition.y + 30, transform.localPosition.z);
                    break;
                case "down":
                    transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                    break;
                case "left":
                    transform.localPosition = new Vector3(-transform.localPosition.x +270, transform.localPosition.y , transform.localPosition.z);
                    break;
                case "right":
                    transform.localPosition = new Vector3(-transform.localPosition.x+330, transform.localPosition.y , transform.localPosition.z);
                    break;

            }
        }
    }
}
