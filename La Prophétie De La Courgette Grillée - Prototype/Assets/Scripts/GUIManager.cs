using UnityEngine;
using UnityEngine.EventSystems;

public class GUIManager : MonoBehaviour
{
    private EventSystem ES;
    private GameObject storeSelected;

    void Start()
    {
        ES = GetComponent<EventSystem>();
        storeSelected = ES.firstSelectedGameObject;
    }

    void Update()
    {
        if (ES.currentSelectedGameObject != storeSelected)
        {
            if (ES.currentSelectedGameObject == null)
            {
                ES.SetSelectedGameObject(storeSelected);
            }
            else
            {
                storeSelected = ES.currentSelectedGameObject;
            }
        }
    }
}
