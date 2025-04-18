using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && onTriggerEnterEvent != null) {
            onTriggerEnterEvent.Invoke();
        }
    }
}
