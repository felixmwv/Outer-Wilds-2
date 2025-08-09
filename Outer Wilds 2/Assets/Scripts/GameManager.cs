using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Distractions[] distractions;
    public DopamineMeter dopamineMeter;
    public CameraAttraction cameraAttraction;
    public float delayBetweenDistractions = 30f;

    private int currentDistractionIndex = 0; // Houd de huidige index bij

    private void Start()
    {
        StartCoroutine(ManageDistractions());
    }
    private System.Collections.IEnumerator ManageDistractions()
    {
        while (true)
        {
            yield return new WaitUntil(() => !dopamineMeter.isDistracted);

            yield return new WaitForSeconds(delayBetweenDistractions);

            if (!dopamineMeter.isDistracted && distractions.Length > 0)
            {
                var chosenDistraction = distractions[currentDistractionIndex];

                if (cameraAttraction != null)
                    cameraAttraction.currentDistraction = chosenDistraction;

                chosenDistraction.DistractionStarter();
                currentDistractionIndex = (currentDistractionIndex + 1) % distractions.Length;
            }
            else
            {
                Debug.Log("A distraction is already active, waiting for it to finish.");
            }
        }
    }
}
