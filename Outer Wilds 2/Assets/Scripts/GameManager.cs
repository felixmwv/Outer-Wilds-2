using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Distractions[] distractions;
    public DopamineMeter dopamineMeter;
    public float delayBetweenDistractions = 30f;

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

            if(!dopamineMeter.isDistracted) // Check if no distraction is currently active
            {
                int randomIndex = Random.Range(0, distractions.Length); // Select a random distraction
                distractions[randomIndex].DistractionStarter(); // Start the distraction
            }
            else
            {
                Debug.Log("A distraction is already active, waiting for it to finish.");
            }
        }
    }
}
