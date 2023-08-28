using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume = 1f;
    public float pitch = 1f;
}

public class AudioManager : MonoBehaviour
{
    public List<Sound> sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        AudioSource.PlayClipAtPoint(sound.clip, Camera.main.transform.position, sound.volume);
    }

}
