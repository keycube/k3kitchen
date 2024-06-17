using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{
    public Recipe currentRecipe;
    public TMP_Text displayText; // UI Text component to display the current word
    public TMP_InputField inputField; // UI InputField component to receive player's input

    private int currentStepIndex;
    private int currentWordIndex;
    private bool isWordCompleted;

    void Start()
    {
        if (currentRecipe != null && displayText != null && inputField != null)
        {
            currentStepIndex = 0;
            currentWordIndex = 0;
            isWordCompleted = false;
            inputField.onEndEdit.AddListener(OnWordEntered);
            StartCoroutine(PlayRecipe());
        }
    }

    IEnumerator PlayRecipe()
    {
        while (currentStepIndex < currentRecipe.steps.Count)
        {
            Step currentStep = currentRecipe.steps[currentStepIndex];
            while (currentWordIndex < currentStep.words.Count)
            {
                string currentWord = currentStep.words[currentWordIndex];
                displayText.text = currentWord;
                isWordCompleted = false;

                // Wait until the player completes the current word
                while (!isWordCompleted)
                {
                    yield return null;
                }

                currentWordIndex++;
            }
            currentWordIndex = 0;
            currentStepIndex++;
        }

        // Recipe completed
        displayText.text = "Recipe completed!";
    }

    void OnWordEntered(string playerInput)
    {
        Step currentStep = currentRecipe.steps[currentStepIndex];
        string currentWord = currentStep.words[currentWordIndex];

        if (playerInput.Trim().Equals(currentWord, System.StringComparison.OrdinalIgnoreCase))
        {
            isWordCompleted = true;
            inputField.text = string.Empty; // Clear input field for the next word
            inputField.ActivateInputField(); // Keep the input field focused
        }
        else
        {
            // Optionally handle incorrect input, e.g., show an error message
            Debug.Log("Incorrect input. Try again.");
        }
    }
}