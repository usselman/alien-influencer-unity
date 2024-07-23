using UnityEngine;

public class UfoCollision : MonoBehaviour
{
    public ParticleSystem UfoExplosion; // Reference to the UFO explosion particle system

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the "Bullet" tag
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Play the explosion effect before destroying the UFO
            if (UfoExplosion != null)
            {
                // Instantiate a new ParticleSystem at the UFO's position because the UFO is about to be destroyed
                // and we won't be able to see the explosion if it's a child of the UFO GameObject.
                ParticleSystem explosionEffect = Instantiate(UfoExplosion, transform.position, Quaternion.identity);
                explosionEffect.Play();
            }

            DestroyUfo(); // Call a method to handle UFO destruction
            GameManager.instance.GameOver();
        }
    }

    void DestroyUfo()
    {
        // Destroys the UFO GameObject
        Destroy(gameObject);
    }
}
