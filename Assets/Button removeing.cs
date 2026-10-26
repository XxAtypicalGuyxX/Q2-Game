using UnityEngine;

public class SwitchRemover : MonoBehaviour
{
    [Tooltip("The GameObject you want to remove when the switch is activated.")]
    public GameObject objectToRemove;

    [Tooltip("Check this if you want to completely delete the object. Uncheck to just hide/disable it.")]
    public bool destroyPermanently = true;

    // Call this method from a UI Button or a Trigger
    public void ActivateSwitch()
    {
        if (objectToRemove != null)
        {
            if (destroyPermanently)
            {
                Destroy(objectToRemove); // Deletes the object from the scene
            }
            else
            {
                objectToRemove.SetActive(false); // Hides/disables the object
            }
        }
    }

    // Optional: If using a 3D physical switch/trigger
    private void OnTriggerEnter(Collider other)
    {
        // Change "Player" to your player's tag if needed
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }
}
