using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class GameData
{
    [Serializable]
    public struct JoueurData
    {
        public float xPos;
        public float yPos;
        public int weapon;
        public float hp;
    }

    [Serializable]
    public struct GameManagerData
    {
        public float money;
        public List<int> items;
        public List<int> countItems;
    }

    [Serializable]
    public struct VendeurData
    {
        public float xPos;
        public float yPos;
        public float money;
        public List<int> items;
    }

    public JoueurData joueurData;
    public GameManagerData gameManagerData;
    public VendeurData vendeurData;

    public GameData()
    {
        vendeurData = new VendeurData()
        {
            items = new List<int>()
        };
        gameManagerData = new GameManagerData()
        {
            items = new List<int>(),
            countItems = new List<int>()
        };
        joueurData = new JoueurData();
    }
}