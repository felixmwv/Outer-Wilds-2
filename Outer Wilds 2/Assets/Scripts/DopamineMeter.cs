using UnityEngine;
using UnityEngine.UI;

public class DopamineMeter : MonoBehaviour
{
    public int dopamine = 100; // Maximum dopamine level
    public int dopamineDecreaseAmount = 1;
    public Slider Slider;
    public bool isPaused = false;
    private void Start()
    {
        // Start the coroutine to decrease dopamine over time
        StartCoroutine(DecreaseDopamineCoroutine());
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

            if (!isPaused)
            {
                dopamine = Mathf.Max(0, dopamine - dopamineDecreaseAmount); // Decrease dopamine but ensure it doesn't go below 0
                UpdateSlider();
            }
        }
    }
    //function for starting the dopamine increase coroutine
    public void AddDopamine(int totalAmount, float duration)
    {
        StartCoroutine(AddDopamineCoroutine(totalAmount, duration));
    }
    //dopamine increase coroutine which will increase the dopamine level over a specified duration
    private System.Collections.IEnumerator AddDopamineCoroutine(int totalAmount, float duration)
    {
        isPaused = true;
        int steps = 10; // Number of steps to increase dopamine
        float stepDuration = duration / steps; // Duration for each step
        int amountPerStep = totalAmount / steps; // Amount to increase in each step

        for (int i = 0; i < steps; i++)
        {
            dopamine = Mathf.Min(100, dopamine + amountPerStep); // Increase dopamine but ensure it doesn't exceed 100
            UpdateSlider();
            yield return new WaitForSeconds(stepDuration); // Wait for the duration of each step
        }
        isPaused = false; // Resume dopamine decrease after the increase is complete
    }
    private void UpdateSlider()
    {
        Slider.value = dopamine; // Update the slider value
    }
}
