using UnityEngine;

public class Distractions : MonoBehaviour
{
    public DopamineMeter dopamineMeter;
    [SerializeField] KeyCode cancelKey = KeyCode.C;

    private Coroutine distractionCoroutine;
    private Coroutine dopamineIncreaseCoroutine;
    [SerializeField] private int distractionDuration = 5; // Duration of the distraction in seconds

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

        dopamineIncreaseCoroutine = dopamineMeter.AddDopamine(25, distractionDuration); // Add dopamine for 5 seconds
        yield return new WaitForSeconds(distractionDuration); // Wait for the distraction duration

        Debug.Log("afleiding afgelopen");
        dopamineMeter.isDistracted = false; // Reset distraction state

        dopamineIncreaseCoroutine = null;
        distractionCoroutine = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(cancelKey))
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
            Debug.Log("Distraction cancelled by player.");
        }
    }
}
