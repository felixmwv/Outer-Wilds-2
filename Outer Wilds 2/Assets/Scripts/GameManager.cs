using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Distractions[] distractions;
    public float delayBetweenDistractions = 10f;

   private void Start()
    {
        StartCoroutine(ManageDistractions());
    }

    private System.Collections.IEnumerator ManageDistractions()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenDistractions);
            if(!Distractions.isDistracted) // Check if no distraction is currently active
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
