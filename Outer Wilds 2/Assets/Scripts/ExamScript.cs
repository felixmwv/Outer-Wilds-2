using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamScript : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers;
        public int correctAnswerIndex;
        [HideInInspector] public int selectedAnswerIndex = -1;
    }

    public Question[] questions = new Question[6];

    // UI references
    public TMP_Text questionText;
    public ToggleGroup answersToggleGroup;
    public Toggle answerTogglePrefab;
    public Transform answersParent;
    public Button nextButton;
    public TMP_Text scoreText;

    private int currentQuestion = 0;
    private int score = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        ShowQuestion();
        nextButton.onClick.AddListener(OnNextClicked);
    }

    void ShowQuestion()
    {
        // Clear old toggles
        foreach (Transform child in answersParent)
            Destroy(child.gameObject);

        answersToggleGroup.allowSwitchOff = true;

        var q = questions[currentQuestion];
        questionText.text = q.questionText;

        for (int i = 0; i < q.answers.Length; i++)
        {
            var toggle = Instantiate(answerTogglePrefab, answersParent);
            toggle.group = answersToggleGroup;
            toggle.isOn = false;
            int index = i;
            toggle.GetComponentInChildren<TMP_Text>().text = q.answers[i];
            toggle.onValueChanged.AddListener((isOn) =>
            {
                if (isOn)
                    q.selectedAnswerIndex = index;
            });
        }
    }

    void OnNextClicked()
    {
        // Check if an answer is selected
        if (questions[currentQuestion].selectedAnswerIndex == -1)
        {
            // Optionally show a warning
            return;
        }

        // Score if correct
        if (questions[currentQuestion].selectedAnswerIndex == questions[currentQuestion].correctAnswerIndex)
            score++;

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowScore();
        }
    }

    void ShowScore()
    {
        questionText.text = "Exam complete!";
        if (score < 3)
            scoreText.text = $"Score: {score} / {questions.Length}\nFailed";
        else
            scoreText.text = $"Score: {score} / {questions.Length}";
        answersParent.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
}
