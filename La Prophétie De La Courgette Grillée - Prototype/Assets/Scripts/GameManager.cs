using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Inventory inventory;
    public static DialogueManager dialogueManager;
    public static QuestManager questManager;
    public static MoneyManager moneyManager;
    public static SaveManager saveManager;
    public static EXPManager expManager;
    
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
        dialogueManager = GetComponent<DialogueManager>();
        questManager = GetComponent<QuestManager>();
        moneyManager = GetComponent<MoneyManager>();
        saveManager = GetComponent<SaveManager>();
        expManager = GetComponent<EXPManager>();
    }
}
