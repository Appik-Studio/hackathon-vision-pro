using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    // Reference to the player GameObject
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        // Check if player reference is set
        if (player != null)
        {
            // Calculate direction from this object to the player
            Vector3 direction = player.position - transform.position;

            // Rotate towards the player
            transform.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            Debug.LogWarning("Player reference is not set. Assign the player GameObject to the 'player' variable in the inspector.");
        }
    }
}
