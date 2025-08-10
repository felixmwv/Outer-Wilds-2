using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: SerializeField] public EventReference Music { get; private set; }

    public static FMODEvents Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        Instance = this;
    }
}