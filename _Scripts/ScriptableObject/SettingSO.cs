using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "SO/SettingSO")]
public class SettingSO : ScriptableObject
{
    private bool _musicCheck;
    private bool _sfxCheck;

    //GETTERS & SETTERS
    public bool MusicCheck
    {
        get => _musicCheck;
        set => _musicCheck = value;
    }
    public bool SfxCheck
    {
        get => _sfxCheck;
        set => _sfxCheck = value;
    }
}
