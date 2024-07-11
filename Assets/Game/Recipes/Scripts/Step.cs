using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Step", menuName = "Recipe/Step")]
public class Step : ScriptableObject
{
    public List<string> words;
}