using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEditorInternal;
using UnityEngine;

public class main : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text text;
    [SerializeField] private string outputtextstart = "You can make:";
    [SerializeField] private Food[] food;
    [SerializeField] private TMP_InputField inputfield;
    private void Start()
    {
        inputfield.onEndEdit.AddListener(OnInputFieldEditFinished);
    }
    private bool CheckFood(Food food, string[] ingredients)
    {
        int lenght = food.ingredients.GetLength(0);
        if (lenght == 0) {
            Debug.Log("No ingredients in meal.");
            return true;
        }
        int checks = 0;
        foreach (string ing1 in ingredients)
        {
            foreach (string ing2 in food.ingredients)
            {
                if (ing2.Equals(ing1) == true)
                {
                    checks++;
                    break;
                }
            }
        }
        if (checks >= lenght)
        {
            return true;
        } else return false;
    }
    private string[] GetFoodName(string[] ingredientslist, out string[] strings)
    {
        List<string> output = new List<string>();
        foreach (Food food in food)
        {
            if (CheckFood(food, ingredientslist) == true)
            {
                output.Add($" {food.name}\n");
            }
        }
        strings = output.ToArray();
        return strings;
    }
    private void OnInputFieldEditFinished(string onestring)
    {
        text.text = outputtextstart + "\n";
        string[] outputtext = { "Ничего." };
        string lastword = onestring.Substring(onestring.LastIndexOf(',') + 1);
        string[] beforemerge = onestring.Split(',');
        string[] ingredientslist = new string[beforemerge.Length+1];
        ingredientslist = beforemerge;
        ingredientslist[beforemerge.Length] = lastword;
        if (GetFoodName(ingredientslist, out outputtext) != null)
        {
            foreach (string str in outputtext)
            {
                text.text += str;
            }
        }
    }
   
}
[Serializable]
public class Food
{
    [field:SerializeField] public string name {  get; private set; }
    [field:SerializeField] public string[] ingredients { get; private set; }
}