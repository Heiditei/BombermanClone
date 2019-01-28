using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                Destroy(this.gameObject);
                break;
            case "Level2":
                Destroy(this.gameObject);
                break;
            case "Level3":
                Destroy(this.gameObject);
                break;
            case "Level4":
                Destroy(this.gameObject);
                break;
            case "Level5":
                Destroy(this.gameObject);
                break;
            case "Level6":
                Destroy(this.gameObject);
                break;
            case "Level7":
                Destroy(this.gameObject);
                break;
        }
    }

    public void PlayMusic()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}