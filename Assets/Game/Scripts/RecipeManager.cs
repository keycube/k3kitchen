using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] recipes;
    public Recipe currentRecipe;
    public TMP_Text displayText;
    public TMP_Text displayScore;
    public TMP_Text displayStars;
    public TMP_Text displayTimer;

    public int score = 0;
    public int stars = 3;
    public int currentStepIndex;

    private int currentLetterIndex;
    private string sentence;
    private bool isRecipeCompleted;
    private float timeRemaining;
    private bool timerRunning;

    void Start()
    {
        // Set the base color of the display text to black
        displayText.color = Color.black;

        displayScore.text = "Score: " + score;
        displayStars.text = "Stars: " + stars;

        SelectRandomRecipe();
    }

    private void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                displayTimer.text = "Time: " + Mathf.Round(timeRemaining).ToString();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                LoseStar();
            }
        }
    }

    private void SelectRandomRecipe()
    {
        int randomIndex = Random.Range(0, recipes.Length);
        currentRecipe = recipes[randomIndex];
        InitializeRecipe();
    }

    private void InitializeRecipe()
    {
        currentStepIndex = 0;
        currentLetterIndex = 0;
        sentence = "";
        isRecipeCompleted = false;

        if (currentRecipe.steps.Count > 0)
        {
            InitializeStep();
            StartTimer();
        }
    }

    private void InitializeStep()
    {
        Debug.Log("Initializing step " + currentStepIndex);
        sentence = "";

        foreach (string word in currentRecipe.steps[currentStepIndex].words)
        {
            sentence += word + " ";
        }

        displayText.text = sentence;
        currentLetterIndex = 0; // Reset the letter index for the new step
    }

    public void TypeLetter(char letter)
    {
        if (isRecipeCompleted) return;

        if (currentLetterIndex < sentence.Length && sentence[currentLetterIndex] == letter)
        {
            currentLetterIndex++;
            Debug.Log("Correct letter");

            // Update displayText to show typed letters with a different style (e.g., grey)
            string typedPart = "<color=grey>" + sentence.Substring(0, currentLetterIndex) + "</color>";
            string remainingPart = sentence.Substring(currentLetterIndex);
            displayText.text = typedPart + remainingPart;

            CheckStepCompletion();
        }
    }

    private void CheckStepCompletion()
    {
        // Check if the entire sentence is typed
        if (currentLetterIndex >= sentence.Length - 1) // -1 to account for the trailing space
        {
            Debug.Log("All words typed!");
            currentStepIndex++;
            if (currentStepIndex < currentRecipe.steps.Count)
            {
                InitializeStep();
            }
            else
            {
                // Handle recipe completion
                Debug.Log("Recipe completed!");
                displayText.text = "Recipe completed!";
                isRecipeCompleted = true;

                score += 2 * currentRecipe.steps.Count;
                displayScore.text = "Score: " + score;

                timerRunning = false;

                // Select a new random recipe
                SelectRandomRecipe();
            }
        }
    }

    private void StartTimer()
    {
        timeRemaining = currentRecipe.timeLimit;
        timerRunning = true;
        displayTimer.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    private void LoseStar()
    {
        stars--;
        displayStars.text = "Stars: " + stars;

        if (stars <= 0)
        {
            SceneManager.LoadScene("Endgame");
        }
    }
}
