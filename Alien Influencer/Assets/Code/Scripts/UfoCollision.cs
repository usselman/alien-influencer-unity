using UnityEngine;

public class UfoCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the "Bullet" tag
        if (other.gameObject.CompareTag("Bullet"))
        {
            DestroyUfo(); // Call a method to handle UFO destruction
        }
    }

    void DestroyUfo()
    {
        // Here, you can add any effects or functionality you want to happen before the UFO is destroyed
        // For example, playing an explosion sound or animation

        Destroy(gameObject); // Destroys the UFO GameObject
    }
}
