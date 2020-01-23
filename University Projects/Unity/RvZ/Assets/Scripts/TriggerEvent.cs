using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This component makes it possible to attach behaviour to a GameObject
/// when a RigidBody2D moves into this object's trigger.
/// </summary>
public class TriggerEvent : MonoBehaviour
{
    [Tooltip("The object->method(params) that will be called when the trigger area is entered.")]
    public UnityEvent onTriggerEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Call ALL the code attached the UnityEvent in the editor.
        onTriggerEntered.Invoke();
    }
}
