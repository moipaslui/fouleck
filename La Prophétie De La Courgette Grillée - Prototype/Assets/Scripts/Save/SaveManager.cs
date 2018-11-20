using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
	public GameObject Joueur;
	public GameObject GameManager;
	public GameObject Vendeur;

	private GameData data;

	private void Start()
	{
		data = new GameData();
		Load();
	}

	private void AddToSave(String varToSave)
	{
		if (varToSave == "player")
		{
			SavePlayer();
		}
	}

	public void SavePlayer()
	{
		BinaryFormatter binform = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		data.joueur = Joueur;
		data.vendeur = Vendeur;
		data.gameManager = GameManager;

		binform.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter binform = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			GameData data = (GameData)binform.Deserialize(file);
			file.Close();

			Joueur = data.joueur;
			Vendeur = data.vendeur;
			GameManager = data.gameManager;
		}
	}
}


[Serializable]
class GameData
{
	public GameObject joueur;
	public GameObject gameManager;
	public GameObject vendeur;
}