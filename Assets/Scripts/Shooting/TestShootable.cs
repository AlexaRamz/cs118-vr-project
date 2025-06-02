using UnityEngine;

public class TestShootable : MonoBehaviour, IShootable
{
    public void OnHit()
    {
        Debug.Log("The object was shot");
    }
}
