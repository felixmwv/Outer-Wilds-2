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

        dopamineMeter.AddDopamine(25, 5f); // Add dopamine for 5 seconds
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
