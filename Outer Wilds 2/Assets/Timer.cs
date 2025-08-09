    using UnityEngine;
    using TMPro;
    using UnityEngine.SceneManagement; // Voeg deze regel toe

    public class Timer : MonoBehaviour
    {
        [SerializeField] TextMeshPro timerText;
        [SerializeField] float remainingTime;
        private bool hasSwitched = false;

        void Update()
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (!hasSwitched)
            {
                remainingTime = 0;
                hasSwitched = true;
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }

            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
