using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    static public int gameMode = 0; // 0:new 1:load

    public int Coin { get { return coin; } set { coin = value; } }
    public string CoinExt { get { return coinExist; } set { coinExist = value; } }

    bool win = false;
    bool fail = false;
    int coin = 0;
    string coinExist = "111";

    AudioSource audioSource;
    public AudioClip ac;

    public Text GameInfo;
    public Text TimeText;
    public Text CoinText;
    public GameObject Btn_save;
    public GameObject[] coinObjs;

    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coin = gameMode == 0 ? 0 : PlayerPrefs.GetInt("Coin", 0);
        coinExist = gameMode == 0 ? "111" : PlayerPrefs.GetString("CoinExt", "111");
        time = gameMode == 0 ? 0 : PlayerPrefs.GetFloat("Time", 0);
        coinHide();
        
    }

    void coinHide()
    {
        for (int i = 0; i < coinExist.Length; i++)
        {
            if (coinExist[i] == '0')
                coinObjs[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!win && !fail)
        {
            TimeUpdate();
            CoinUpdate();
            BGChange();
        }
        
        Restart();
    }

    //勝利紀錄
    void Record()
    {
        RecordState rs = new RecordState();
        rs.time = time;
        rs.coin = coin;

        string saveString = JsonUtility.ToJson(rs);
        saveString += ",";

        StreamWriter file = new StreamWriter(Path.Combine(Application.persistentDataPath, "RecordState"), true);
        file.Write(saveString);
        file.Close();
    }

    void CoinUpdate()
    {
        CoinText.text = coin.ToString();
    }
    void TimeUpdate()
    {
        time += Time.deltaTime;
        TimeText.text = time.ToString("f2");
    }
    void BGChange()
    {
        Color col = Color.Lerp(Color.white, Color.gray, Mathf.PingPong(Time.time*0.1f, 1));
        RenderSettings.ambientLight = col;
    }


    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            AssetBundle.UnloadAllAssetBundles(true);
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    //out of border
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!win && collision.gameObject.CompareTag("Player"))
        {
            Lose();
            Destroy(collision.gameObject);
        }
    }

    public void Win()
    {
        win = true;
        GameInfo.text = "Victory !";
        GameInfo.color = Color.red;
        Btn_save.SetActive(false);
        Record();
    }
    public void Lose()
    {
        fail = true;
        audioSource.Stop();
        audioSource.PlayOneShot(ac);
        Btn_save.SetActive(false);
        GameInfo.text = "Loser!";
    }

    public void SaveGame()
    {
        Btn_save.GetComponent<Animator>().SetTrigger("click");
        //角色位置
        PlayerPrefs.SetFloat("PosX", GameObject.FindGameObjectWithTag("Player").transform.position.x);
        PlayerPrefs.SetFloat("PosY", GameObject.FindGameObjectWithTag("Player").transform.position.y);
        //金幣數量位置
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetString("CoinExt", coinExist);
        //經過時間
        PlayerPrefs.SetFloat("Time", time);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }
}
