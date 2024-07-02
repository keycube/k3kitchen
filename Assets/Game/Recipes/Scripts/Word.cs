using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Word", menuName = "Recipe/Word")]
public class Word : ScriptableObject
{
    public string word;
    private int typeIndex;
    
    public Word(string word)
    {
        this.word = word;
        typeIndex = 0;
    }

    public char GetNextLetter()
    {
        return word[typeIndex];
    }

    public void TypeLetter()
    {
        typeIndex++;
        //Remove the letter on screen
    }

    public bool WordTyped()
    {
        bool wordTyped = (typeIndex >= word.Length);
        if (wordTyped)
        {
            typeIndex = 0;
            //Remove the word on screen
        }
        return wordTyped;
    }
}
