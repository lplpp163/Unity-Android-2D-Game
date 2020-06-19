using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DeleteRecord : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { File.Delete(Path.Combine(Application.persistentDataPath, "RecordState")); SceneManager.LoadScene(0); });
    }
}
