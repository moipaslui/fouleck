using UnityEngine;

public class Money : MonoBehaviour
{
    public float amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        FindObjectOfType<MoneyManager>().AddMoney(amount);
        Destroy(this.gameObject);
    }
}
