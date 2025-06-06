using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if ((other.tag == "Player" || other.tag == "MainCamera") && onTriggerEnterEvent != null) {
            onTriggerEnterEvent.Invoke();
        }
    }
}
