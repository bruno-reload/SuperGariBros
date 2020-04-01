using UnityEngine;
public class ItemPool : MonoBehaviour
{
    protected bool able = false;
    protected bool active = false;

    public bool Able
    {
        set { able = value; }
        get { return able; }
    }public bool Active
    {
        set { active = value; }
        get { return active; }
    }
}
