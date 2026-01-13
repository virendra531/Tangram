using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioClip click;
    AudioClip bump;
    AudioClip pop;

    AudioSource source;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        source = gameObject.AddComponent<AudioSource>();

        click = Resources.Load<AudioClip>("Audio/Click");
        bump = Resources.Load<AudioClip>("Audio/Bump");
        pop = Resources.Load<AudioClip>("Audio/Pop");
    }

    public void PlayClick()
    {
        if (!click) return;
        source.pitch = Random.Range(0.95f, 1.05f);
        source.PlayOneShot(click);
    }

    public void PlayBump()
    {
        if (bump) source.PlayOneShot(bump);
    }

    public void PlayPop()
    {
        if (pop) source.PlayOneShot(pop);
    }
}
