using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int id;
    public string p_name;
    public float pos_x;
    public float pos_y;
    public float pos_z;
    public int HP;
    public int MP;
    public List<string> inventory;
}

public class PlayerControllerA : MonoBehaviour
{
    public string playerName = "Player1";
    public int HP = 15;
    public int MP = 20;
    public float speed = 5f;

    public List<string> inventory = new List<string>(); // <<< BURASI EKLENDİ
    private string savePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "save1.json");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.01f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.01f, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0.01f, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, -0.01f, 0);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadAndTeleport();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "SquareB")
        {
            AddItems();
        }
        else if (collision.gameObject.name == "SquareC")
        {
            Heal();
        }
    }

    void AddItems()
    {
        inventory.Add("木の棒"); // tahta sopa
        inventory.Add("鍋の蓋"); // tencere kapağı
        inventory.Add("ハーブ"); // bitki
        Debug.Log("Item Added: 木の棒, 鍋の蓋, ハーブ");
    }

    void Heal()
    {
        HP = 100;
        MP = 100;
        Debug.Log("HP =100 MP MP=100");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "SquareB" || collision.gameObject.name == "SquareC")
        {
            Vector3 pos = collision.transform.position;
            SavePosition(pos);
        }
        if (collision.gameObject.name == "SquareB")
        {
            AddItems();
        }
        else if (collision.gameObject.name == "SquareC")
        {
            Heal();
        }

        if (collision.gameObject.name == "SquareB" || collision.gameObject.name == "SquareC")
        {
            SaveGame();
        }


    }

 

    void SaveGame()
    {
        SaveData data = new SaveData
        {
            id = 1,
            p_name = playerName,
            pos_x = transform.position.x,
            pos_y = transform.position.y,
            pos_z = transform.position.z,
            HP = this.HP,
            MP = this.MP,
            inventory = new List<string>(this.inventory)
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game Saved " + savePath);
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