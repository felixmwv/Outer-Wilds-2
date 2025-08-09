using UnityEngine;

public class Distractions : MonoBehaviour
{
    public DopamineMeter dopamineMeter;
    [SerializeField] private int distractionDuration = 5; // Duration of the distraction in seconds

    private Coroutine distractionCoroutine;
    private Coroutine dopamineIncreaseCoroutine;

    public void DistractionStarter()
    {
        if (!dopamineMeter.isDistracted)
        {
            distractionCoroutine = StartCoroutine(DistractionCoroutine());
        }
    }

    private System.Collections.IEnumerator DistractionCoroutine()
    {
        dopamineMeter.isDistracted = true;
        Debug.Log("afleiding bezig");

        dopamineIncreaseCoroutine = dopamineMeter.AddDopamine(dopamineMeter.dopamineIncreaseAmount, distractionDuration); // Add dopamine for 5 seconds
        yield return new WaitForSeconds(distractionDuration); // Wait for the distraction duration

        Debug.Log("afleiding afgelopen");
        dopamineMeter.isDistracted = false; // Reset distraction state

        dopamineIncreaseCoroutine = null;
        distractionCoroutine = null;
    }
    public void ForceStopDistraction()
    {
        if (distractionCoroutine != null)
        {
            StopCoroutine(distractionCoroutine);
            distractionCoroutine = null;
        }
        if (dopamineIncreaseCoroutine != null)
        {
            dopamineMeter.CancelCurrentIncrease();
            dopamineIncreaseCoroutine = null;
        }
        dopamineMeter.isDistracted = false;
        Debug.Log("Distraction forcefully stopped.");
    }
}
