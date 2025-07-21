using UnityEngine;
using UnityEngine.UI;

public class DopamineMeter : MonoBehaviour
{
    public int dopamine = 100; // Maximum dopamine level
    public int dopamineDecreaseAmount = 1;
    public Slider Slider;
    private void Start()
    {
        // Start the coroutine to decrease dopamine over time
        StartCoroutine(DecreaseDopamineOverTime());
    }
    public void TestDopamine()
    {
        dopamine = Mathf.Min(100, dopamine + 25);
        UpdateSlider();
    }
    private System.Collections.IEnumerator DecreaseDopamineOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            dopamine = Mathf.Max(0, dopamine - dopamineDecreaseAmount); // Decrease dopamine but ensure it doesn't go below 0
            UpdateSlider();
        }
    }
    private void UpdateSlider()
    {
        Slider.value = dopamine; // Update the slider value
    }
}
