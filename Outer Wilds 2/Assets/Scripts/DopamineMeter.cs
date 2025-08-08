using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Or .HighDefinition if using HDRP

public class DopamineMeter : MonoBehaviour
{
    public int dopamine = 100;
    public int dopamineDecreaseAmount = 1;
    public Slider Slider;
    public bool isDistracted = false;

    // Add reference to your Volume
    public Volume volume;
    public int dopamineThreshold = 50; // Threshold for dopamine effects

    // Store original values
    private float originalFocalLength = 50f;
    private float originalVignette = 0.2f;
    private float originalLensDistortion = 0f;
    private float originalChromaticAberration = 0f;

    // Target values when dopamine is 0
    [SerializeField] private float targetFocalLength = 150f;
    [SerializeField] private float targetVignette = 0.4f;
    [SerializeField] private float targetLensDistortion = -0.3f;
    [SerializeField] private float targetChromaticAberration = 0.5f;

    // Cached effect components
    private DepthOfField dof;
    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    private Coroutine addCoroutine;

    private void Start()
    {
        // Get effect components from the VolumeProfile
        if (volume != null && volume.profile != null)
        {
            volume.profile.TryGet(out dof);
            volume.profile.TryGet(out vignette);
            volume.profile.TryGet(out lensDistortion);
            volume.profile.TryGet(out chromaticAberration);
        }
        StartCoroutine(DecreaseDopamineCoroutine());
    }

    private void Update()
    {
        UpdateVolumeEffects();
    }

    private void UpdateVolumeEffects()
    {
        if (dof != null && vignette != null && lensDistortion != null && chromaticAberration != null)
        {
            float t = Mathf.InverseLerp(dopamineThreshold, 0, Mathf.Clamp(dopamine, 0, dopamineThreshold));
            // Lerp from original to target as dopamine drops from 50 to 0
            dof.focalLength.value = Mathf.Lerp(originalFocalLength, targetFocalLength, t);
            vignette.intensity.value = Mathf.Lerp(originalVignette, targetVignette, t);
            lensDistortion.intensity.value = Mathf.Lerp(originalLensDistortion, targetLensDistortion, t);
            chromaticAberration.intensity.value = Mathf.Lerp(originalChromaticAberration, targetChromaticAberration, t);
        }
    }

    public void TestDopamine()
    {
        dopamine = Mathf.Min(100, dopamine + 25);
        UpdateSlider();
    }
    //dopamine decrease coroutine which will decrease the dopamine level over time
    private System.Collections.IEnumerator DecreaseDopamineCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second

            if (!isDistracted)
            {
                dopamine = Mathf.Max(0, dopamine - dopamineDecreaseAmount); // Decrease dopamine but ensure it doesn't go below 0
                UpdateSlider();
            }
        }
    }
    //function for starting the dopamine increase coroutine
    public Coroutine AddDopamine(int totalAmount, float duration)
    {
        if (addCoroutine != null)
        {
            StopCoroutine(addCoroutine);
        }
        addCoroutine = StartCoroutine(AddDopamineCoroutine(totalAmount, duration));
        return addCoroutine;
    }
    //dopamine increase coroutine which will increase the dopamine level over a specified duration
    private System.Collections.IEnumerator AddDopamineCoroutine(int totalAmount, float duration)
    {
        int steps = 10; // Number of steps to increase dopamine
        float stepDuration = duration / steps; // Duration for each step
        int amountPerStep = totalAmount / steps; // Amount to increase in each step

        for (int i = 0; i < steps; i++)
        {
            dopamine = Mathf.Min(100, dopamine + amountPerStep); // Increase dopamine but ensure it doesn't exceed 100
            UpdateSlider();
            yield return new WaitForSeconds(stepDuration); // Wait for the duration of each step
        }

        addCoroutine = null;
    }
    public void CancelCurrentIncrease()
    {
        if (addCoroutine != null)
        {
            StopCoroutine(addCoroutine); // Stop the current dopamine increase coroutine
            addCoroutine = null; // Reset the coroutine reference
        }
    }
    private void UpdateSlider()
    {
        Slider.value = dopamine; // Update the slider value
    }
}
