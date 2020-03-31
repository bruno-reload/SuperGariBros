using UnityEngine;
public class ItemPool : MonoBehaviour
{
    protected bool able = false;

    public bool Able
    {
        set { able = value; }
        get { return able; }
    }
}
