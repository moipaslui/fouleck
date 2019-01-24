using UnityEngine;

public class ActiveEnnemiAT : ActionTrigger
{
    public override void Trigger()
    {
        Debug.Log("a");
        GetComponent<EnnemiController>().enabled = true;
        gameObject.layer = 10;
        gameObject.tag = "ennemi";

        base.Trigger();
    }
}
