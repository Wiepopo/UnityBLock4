using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Object", menuName = "Assets/New Audio Object")]
public class AudioObject : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
    
}