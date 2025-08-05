using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DynamicDOF : MonoBehaviour
{
    private float focusDistance;
    private Volume volume;
    private VolumeProfile volumeProfile;
    private DepthOfField depthOfField;

    private RaycastHit hit;
    private void Start()
    {
        volume = gameObject.GetComponent<Volume>();
        volume.profile.TryGet<DepthOfField>(out depthOfField);
    }

    void Update()
    {
        Physics.Raycast(transform.position, transform.forward, out hit);
        if (hit.collider != null)
        {
            depthOfField.focusDistance.value = hit.distance;
        }
    }
}
