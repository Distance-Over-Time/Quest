using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSolutions : MonoBehaviour
{
    // TODO: replace with what Shannon is creating as recipes story-wise
    [SerializeField] private Dictionary<string, string[]> recipes;

    void Awake() {
        recipes = new Dictionary<string, string[]>() {
            {"$midItem0", new string[] {"0", "1", "2", "3"} },
            {"$midItem1", new string[] {"4", "5", "6", "7"} },
            {"$midItem2", new string[] {"2", "4", "6", "5"} },
            {"$midItem3", new string[] {"1", "3", "3", "7"} },
            {"finalSolution", new string[] {"8", "9", "10", "11"} } // This would be the final recipe to complete the game
        };
    }

    public Dictionary<string, string[]> GetRecipes() {
        return recipes;
    }
}
