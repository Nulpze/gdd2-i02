using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int MaxScore = 0;
    public static GameManager Instance;

    public AudioSource destroyEnemySound;
    public GameObject explosionPrefab;

    public void Awake()
    {
        // Register the singleton
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of GameManager!");
        }
        Instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExplosionEffect(Vector3 position)
    {
        GameObject newParticleSystem = Instantiate(
         explosionPrefab,
         position,
         Quaternion.identity
       );
       Destroy(newParticleSystem, 2f);
    }

    public void PlayDestroyEnemy()
    {
        PlaySound(destroyEnemySound);
    }

    private void PlaySound(AudioSource source)
    {
        if (source)
        {
            source.Play();
        }
    }
}
