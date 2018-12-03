using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private GameData data;
    public GameObject joueur;
    public Seller vendeur;
    public ListItems listItems;
    public GameObject itemPrefab;

    private void Awake()
    {
        data = new GameData();
        //LoadData();
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Create);
        data = new GameData();

        data.joueurData.xPos = joueur.transform.position.x;
        data.joueurData.yPos = joueur.transform.position.y;
        data.joueurData.hp = joueur.GetComponent<PlayerHealthManager>().currentHP;

        data.gameManagerData.money = GetComponent<MoneyManager>().currentMoney;

        data.vendeurData.xPos = vendeur.transform.position.x;
        data.vendeurData.yPos = vendeur.transform.position.y;
        data.vendeurData.money = vendeur.currentMoney;

        data.joueurData.weapon = -1;
        for (int i = 0; i < listItems.items.Count; i++)
        {
            if(listItems.items[i] == joueur.GetComponent<WeaponOnPlayer>().arme)
                data.joueurData.weapon = i;

            for(int y = 0; y < GetComponent<Inventory>().items.Count; y++)
            {
                if(listItems.items[i] == GetComponent<Inventory>().items[y].item)
                {
                    data.gameManagerData.items.Add(i);
                    data.gameManagerData.countItems.Add(GetComponent<Inventory>().items[y].count);
                }
            }

            foreach (Item item in vendeur.itemsToSell)
            {
                if (listItems.items[i] == item)
                {
                    data.vendeurData.items.Add(i);
                }
            }
        }

        foreach(Quest quest in GetComponent<QuestManager>().quests)
        {
            data.quests.Add(quest.isActive);

            data.questTriggers.Add(new List<bool>());
            foreach(QuestTrigger trigger in quest.questTriggers)
            {
                data.questTriggers[data.questTriggers.Count - 1].Add(trigger.isActive);
            }
        }

        bf.Serialize(file, data);
        file.Close();

        Debug.Log("Saved !");
    }

    public void LoadData()
    {
        if(File.Exists(Application.persistentDataPath + "/save.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
            file.Close();

            joueur.transform.position = new Vector2(data.joueurData.xPos, data.joueurData.yPos);
            joueur.GetComponent<PlayerHealthManager>().currentHP = data.joueurData.hp;
            if(data.joueurData.weapon != -1)
                joueur.GetComponent<WeaponOnPlayer>().arme = (Weapon)listItems.items[data.joueurData.weapon];

            GetComponent<MoneyManager>().AddMoney(-GetComponent<MoneyManager>().currentMoney);
            GetComponent<MoneyManager>().AddMoney(data.gameManagerData.money);

            vendeur.transform.position = new Vector3(data.vendeurData.xPos, data.vendeurData.yPos, 0);
            vendeur.currentMoney = data.vendeurData.money;

            GetComponent<Inventory>().RemoveAll();
            for (int i = 0; i < data.gameManagerData.items.Count; i++)
            {
                for (int y = 0; y < data.gameManagerData.countItems[i]; y++)
                    GetComponent<Inventory>().Add(listItems.items[data.gameManagerData.items[i]]);
            }

            vendeur.itemsToSell.Clear();
            for (int i = 0; i < data.vendeurData.items.Count; i++)
            {
                vendeur.itemsToSell.Add(listItems.items[data.vendeurData.items[i]]);
            }

            GetComponent<QuestManager>().LoadQuests(data.quests, data.questTriggers);

            Debug.Log("Loaded !");
        }
    }
}
