using UnityEngine;
using UnityEngine.UI;

public class DopamineMeter : MonoBehaviour
{
    public int dopamine = 100; // Maximum dopamine level
    public Slider slider;

    public void UpdateDopamine()
    {
        dopamine--;
        slider.value = dopamine;
    }
}
