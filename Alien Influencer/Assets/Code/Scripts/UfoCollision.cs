using UnityEngine;

public class UfoCollision : MonoBehaviour
{
    public ParticleSystem UfoExplosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            if (UfoExplosion != null)
            {
                ParticleSystem explosionEffect = Instantiate(UfoExplosion, transform.position, Quaternion.identity);
                explosionEffect.Play();
            }

            DestroyUfo();
            GameManager.Instance.GameOver();
        }
    }

    void DestroyUfo()
    {
        Destroy(gameObject);
    }
}
