using UnityEngine;

public class DistractionTemp : MonoBehaviour
{
public GameObject distractionObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log ("space pressed");
        }
    }
}
