using UnityEngine;
using System.IO;
using System;

[Serializable]
public class SaveData1
{
    public int id;
    public string p_name;
    public float pos_x;
    public float pos_y;
    public float pos_z;
}

public class PlayerControllerC : MonoBehaviour
{
    private string savePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.01f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.01f, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0.01f, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -0.01f, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadAndTeleport();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1");
        if (collision.gameObject.name == "SquareB" || collision.gameObject.name == "SquareD")
        {
            Vector3 pos = collision.transform.position;
            SavePosition(pos);
        }
    }
    void SavePosition(Vector3 pos)
    {
        SaveData data = new SaveData
        {
            id = 1,
            p_name = "p-1",
            pos_x = pos.x,
            pos_y = pos.y,
            pos_z = pos.z,
        };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
    }
    void LoadAndTeleport()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("ファイルが見つかりません"); return;
        }
        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        transform.position = new Vector3(data.pos_x, data.pos_y, data.pos_z);
    }
}