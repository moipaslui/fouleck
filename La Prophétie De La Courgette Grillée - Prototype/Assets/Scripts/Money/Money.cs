using UnityEngine;

public class Money : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        FindObjectOfType<MoneyManager>().addMoney(amount);
        Destroy(this.gameObject);
    }
}
