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
            {"solution0", new string[] {"0", "1", "2", "3"} },
            {"solution1", new string[] {"4", "5", "6", "7"} },
            {"solution2", new string[] {"2", "4", "6", "6"} },
            {"solution3", new string[] {"1", "3", "3", "7"} },
            {"solution4", new string[] {"3", "3", "3", "3"} } // This would be the final recipe to complete the game
        };
    }

    public Dictionary<string, string[]> GetRecipes() {
        return recipes;
    }
}
