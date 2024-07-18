using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordInput : MonoBehaviour
{

    public RecipeManager recipeManager;
    public Recipe recipe;
    public Step step;

    void Start()
    {
        recipe = recipeManager.currentRecipe;
        step = recipe.steps[recipeManager.currentStepIndex];
    }

    void Update()
    {
        foreach (char letter in Input.inputString)
        {
            recipeManager.TypeLetter(letter);
            Debug.Log(letter);
        }
    }
}
