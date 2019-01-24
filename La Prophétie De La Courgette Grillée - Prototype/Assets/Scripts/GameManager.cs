using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Inventory inventory;
    public static DialogueManager dialogueManager;
    public static QuestManager questManager;
    public static MoneyManager moneyManager;
    public static SaveManager saveManager;
    public static EXPManager expManager;
    public static MusicManager musicManager;
    public static ItemManager itemManager;
    
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        dialogueManager = GetComponent<DialogueManager>();
        questManager = GetComponent<QuestManager>();
        moneyManager = GetComponent<MoneyManager>();
        saveManager = GetComponent<SaveManager>();
        expManager = GetComponent<EXPManager>();
        musicManager = GetComponentInChildren<MusicManager>();
        itemManager = GetComponent<ItemManager>();
    }
}
