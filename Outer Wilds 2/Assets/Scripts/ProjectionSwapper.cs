using UnityEngine;

public class ProjectionSwapper : MonoBehaviour
{
    public Material[] materials;
    public Renderer targetRenderer;
    public float switchInterval = 30f;

    private int currentIndex = 0;
    private float timer = 0f;

    void Start()
    {
        if (materials.Length > 0 && targetRenderer != null)
        {
            targetRenderer.material = materials[0];
        }
    }

    void Update()
    {
        if (materials.Length == 0 || targetRenderer == null)
            return;

        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            timer = 0f;
            currentIndex = (currentIndex + 1) % materials.Length;
            targetRenderer.material = materials[currentIndex];
        }
    }
}
