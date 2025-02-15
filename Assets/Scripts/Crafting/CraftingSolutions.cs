using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSolutions : MonoBehaviour
{
    private string nullValue  = "-1";

    [SerializeField] private Dictionary<string, string[]> recipes;
    [SerializeField] private GameObject[] ingredientObjs;

    /* 
    -------- VARIABLE REFERENCES FOR STORY --------
    
    INGREDIENTS
        $matItem0 : water       (0)
        $matItem1 : chamomile   (1)
        $matItem2 : oats        (2)
        $matItem3 : wolfsbane   (3)
        $matItem4 : honey       (4)
        $matItem5 : beeswax     (5)
        $matItem6 : tomato      (6)
        $matItem7 : lemon       (7)

    KEY ITEMS
        $keyItem0 : TEA
            Ingredients | $matItem0,  $matItem1
                          (water)     (camomile)

        $keyItem1 : GROG
            Ingredients | $matItem0,  $matItem4
                          (water)     (honey)
        
        $keyItem2 : PORRIDGE
            Ingredients | $matItem0,  $matItem2,  $matItem4
                          (water)     (oats)      (honey)
        
        $keyItem3 : SALVE
            Ingredients | $matItem1,  $matItem2,  $matItem3,  $matItem5
                          (chamomile) (oats)      (wolfsbane) (beeswax)
        
        $finalKey : POTION
            Ingredients | $matItem3,  $matItem4,  $matItem6,  $matItem7
                          (wolfsbane) (honey)     (tomato)    (lemon)
    */

    // NOTE: The use of nullValue is to replace actual null values
    //       This keeps my original crafting system code in tact

    void Awake() {
        recipes = new Dictionary<string, string[]>() {
            {"$keyItem0", new string[] {"0", "1", nullValue, nullValue} },    // TEA
            {"$keyItem1", new string[] {"0", "4", nullValue, nullValue} },    // GROG
            {"$keyItem2", new string[] {"0", "2", "4", nullValue} },          // PORRIDGE
            {"$keyItem3", new string[] {"1", "2", "3", "5"} },                // SALVE
            {"$finalKey", new string[] {"3", "4", "6", "7"} }                 // POTION (FINAL)
        };
    }

    public Dictionary<string, string[]> GetRecipes() {
        return recipes;
    }

    public GameObject[] GetAllIngredientObjs() {
        return ingredientObjs;
    }

    public string GetGameNullValue() {
        return nullValue;
    }
}
