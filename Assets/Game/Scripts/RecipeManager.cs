using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public Recipe[] recipes;
    public Recipe currentRecipe;
    public TMP_Text displayText;
    public int currentStepIndex;
    private int currentLetterIndex;
    private string sentence;
    private bool isRecipeCompleted;

    void Start()
    {
        // Set the base color of the display text to black
        displayText.color = Color.black;

        SelectRandomRecipe();
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

                // Select a new random recipe
                SelectRandomRecipe();
            }
        }
    }
}
