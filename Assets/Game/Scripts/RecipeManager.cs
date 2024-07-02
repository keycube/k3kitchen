using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeManager : MonoBehaviour
{

    public Recipe[] recipes;

    public Recipe currentRecipe;
    public TMP_Text displayText; // UI Text component to display the current word
    public TMP_Text displayText2;

    public int currentStepIndex;

    private int currentWordIndex;

    private bool hasActiveWord;
    private Word activeWord;

    void Start()
    {
        currentRecipe = recipes[0];
        currentStepIndex = 0;
        currentWordIndex = 0;
        hasActiveWord = false;
        activeWord = null;
            
        displayText.text = currentRecipe.steps[currentStepIndex].words[currentWordIndex].word;
        displayText2.text = currentRecipe.steps[currentStepIndex].words[currentWordIndex+1].word;
    }

    public void TypeLetter(char letter)
    {
        if (hasActiveWord)
        {
            if (activeWord.GetNextLetter() == letter)
            {
                activeWord.TypeLetter();
            }
        }
        else
        {
            foreach (Word word in currentRecipe.steps[currentStepIndex].words)
            {
                if (word.GetNextLetter() == letter)
                {
                    activeWord = word;
                    hasActiveWord = true;
                    word.TypeLetter();
                    break;
                }
            }
        }

        if (hasActiveWord && activeWord.WordTyped())
        {
            hasActiveWord = false;
            activeWord = null;
        }
    }
}