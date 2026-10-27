using UnityEngine;

public class CharacterPush : MonoBehaviour
{
    [Tooltip("Adjust how hard the player can push objects")]
    public float pushPower = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // Check if the object has a Rigidbody and isn't kinematic
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We don't want to push objects below us (standing on top of things)
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        // Calculate push direction from move direction (only pushing horizontally)
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply the force to the object's Rigidbody
        body.linearVelocity = pushDir * pushPower;
    }
}