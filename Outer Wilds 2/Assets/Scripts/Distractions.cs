using UnityEngine;

public class Distractions : MonoBehaviour
{
    public DopamineMeter dopamineMeter;
    public static bool isDistracted = false;

    public void DistractionStarter()
    {
        if (!isDistracted)
        {
            StartCoroutine(DistractionCoroutine());
        }
    }

    private System.Collections.IEnumerator DistractionCoroutine()
    {
        isDistracted = true;
        Debug.Log("afleiding bezig");
        dopamineMeter.AddDopamine(25, 5f); // Add dopamine for 5 seconds
        yield return new WaitForSeconds(5f); // Wait for the distraction duration
        Debug.Log("afleiding afgelopen");
        isDistracted = false; // Reset distraction state
    }
}
