using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public List<Step> steps;
}