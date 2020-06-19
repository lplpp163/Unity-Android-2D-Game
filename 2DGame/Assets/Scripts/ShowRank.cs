using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ShowRank : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text rankText = GetComponent<Text>();
        try
        {
            StreamReader file = new StreamReader(Path.Combine(Application.persistentDataPath, "RecordState"));
            string loadJson = file.ReadToEnd();
            loadJson = loadJson.Remove(loadJson.Length - 1);
            loadJson = "{\"Items\":[" + loadJson + "]}";
            file.Close();

            RecordArray loadData = new RecordArray();
            loadData = JsonUtility.FromJson<RecordArray>(loadJson);

            rankText.text = "Rank\t\tTime\t\tCoin\n";

            Array.Sort(loadData.Items, delegate (RecordState x, RecordState y) { return x.time.CompareTo(y.time); });
            for (int i = 0; i < loadData.Items.Length; i++)
            {
                rankText.text += ((i + 1) + "\t\t\t"
                    + loadData.Items[i].time.ToString("f2") + "\t\t"
                    + loadData.Items[i].coin + "\n");
            }
        }
        catch (FileNotFoundException) { }
    }
}
