using UnityEngine;

public class InstantiateAT : ActionTrigger
{
    public GameObject prefab;
    public static bool a;

    public override void Trigger()
    {
        Instantiate(prefab, transform);

        base.Trigger();
    }
}
