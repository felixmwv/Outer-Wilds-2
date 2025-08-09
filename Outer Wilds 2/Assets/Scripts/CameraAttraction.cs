using Unity.Cinemachine;
using UnityEngine;

public class CameraAttraction : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public DopamineMeter dopamineMeter;
    public Transform distractionTarget;
    public float attractionStrength = 5f;
    public float releaseAngle = 45f;
    public AnimationCurve attractionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private CinemachinePanTilt panTilt;
    private bool distractionActive = false;
    private float initialAngle = 0f;

    private CinemachineInputAxisController inputAxisController;
    private float? originalGain0 = null;
    private float? originalGain1 = null;
    public float distractedGain = 0.1f; // Lager getal voor tijdens afleiding

    private bool wasDistracted = false; // Nieuw: onthoud vorige status

    // FOV zoom
    private float originalFOV = 50f;
    public float zoomedFOV = 20f;
    private Coroutine fovCoroutine;

    public Distractions currentDistraction; // Assign this when a distraction starts

    void Start()
    {
        panTilt = virtualCamera.GetComponent<CinemachinePanTilt>();
        inputAxisController = virtualCamera.GetComponent<CinemachineInputAxisController>();
        originalFOV = virtualCamera.Lens.FieldOfView;
    }

    void Update()
    {
        if (dopamineMeter.isDistracted && distractionTarget != null && panTilt != null)
        {
            // Sla originele gain-waarden op bij eerste afleiding
            if (!distractionActive && inputAxisController != null && inputAxisController.Controllers.Count >= 2)
            {
                if (originalGain0 == null) originalGain0 = inputAxisController.Controllers[0].Input.Gain;
                if (originalGain1 == null) originalGain1 = inputAxisController.Controllers[1].Input.Gain;

                // Respecteer het teken van de originele gain
                inputAxisController.Controllers[0].Input.Gain = Mathf.Sign(originalGain0.Value) * Mathf.Abs(distractedGain);
                inputAxisController.Controllers[1].Input.Gain = Mathf.Sign(originalGain1.Value) * Mathf.Abs(distractedGain);
            }

            // Start FOV zoom-in bij begin afleiding
            if (!distractionActive)
            {
                if (fovCoroutine != null) StopCoroutine(fovCoroutine);
                fovCoroutine = StartCoroutine(AnimateFOV(virtualCamera.Lens.FieldOfView, zoomedFOV, 1f));
            }

            Vector3 toTarget = distractionTarget.position - virtualCamera.transform.position;
            Vector3 cameraForward = virtualCamera.transform.forward;
            float angle = Vector3.Angle(cameraForward, toTarget);

            if (!distractionActive)
            {
                initialAngle = angle;
                distractionActive = true;
            }

            Quaternion lookRot = Quaternion.LookRotation(toTarget, Vector3.up);
            Vector3 lookEuler = lookRot.eulerAngles;

            float angleProgress = 1f - Mathf.Clamp01(angle / Mathf.Max(initialAngle, 0.01f));
            float curveValue = attractionCurve.Evaluate(angleProgress);

            float lerpSpeed = Time.deltaTime * attractionStrength * curveValue;
            float newPan = Mathf.LerpAngle(panTilt.PanAxis.Value, lookEuler.y, lerpSpeed);
            float newTilt = Mathf.LerpAngle(panTilt.TiltAxis.Value, lookEuler.x, lerpSpeed);
            panTilt.PanAxis.Value = newPan;
            panTilt.TiltAxis.Value = newTilt;

            if (distractionActive && angle > releaseAngle && angle > initialAngle + 1f)
            {
                if (currentDistraction != null)
                    currentDistraction.ForceStopDistraction();
                distractionActive = false;
            }
        }
        else
        {
            distractionActive = false;
        }

        // Reset gain en FOV als de afleiding net is gestopt
        if (wasDistracted && !dopamineMeter.isDistracted && inputAxisController != null && inputAxisController.Controllers.Count >= 2)
        {
            if (originalGain0.HasValue)
                inputAxisController.Controllers[0].Input.Gain = originalGain0.Value;
            if (originalGain1.HasValue)
                inputAxisController.Controllers[1].Input.Gain = originalGain1.Value;

            // Start FOV zoom-out
            if (fovCoroutine != null) StopCoroutine(fovCoroutine);
            fovCoroutine = StartCoroutine(AnimateFOV(virtualCamera.Lens.FieldOfView, originalFOV, 1f));
        }

        // Update de status voor de volgende frame
        wasDistracted = dopamineMeter.isDistracted;
    }

    private System.Collections.IEnumerator AnimateFOV(float from, float to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            virtualCamera.Lens.FieldOfView = Mathf.Lerp(from, to, elapsed / duration);
            yield return null;
        }
        virtualCamera.Lens.FieldOfView = to;
    }
}