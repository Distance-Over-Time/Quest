using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class KeyItemReaction : ItemReaction
{
    [SerializeField] private Image checkmark;
    private string keyItemYarnName;
    private string[] thisKeyRecipe;
    private CraftingSolutions solutions;
    private GameObject[] ingredientObjs;
    private string noIngredient;
    

    // Start is called before the first frame update
    void Start()
    {
        solutions = GameObject.Find("CraftSolutionManager").GetComponent<CraftingSolutions>();
        ingredientObjs = solutions.GetAllIngredientObjs();
        noIngredient = solutions.GetGameNullValue();

        keyItemYarnName = transform.parent.name;
        var recipes = solutions.GetRecipes();
        if (recipes.TryGetValue(keyItemYarnName, out string[] recipe)) {
            thisKeyRecipe = recipe;
        }
        else {
            Debug.Log(keyItemYarnName + " couldn't be matched to recipe dictionary from CraftSolutionManager");
        }

        // Hide checkmark image
        SetCraftedStatus(false);

    }

    void OnEnable() {
        if (thisKeyRecipe != null) {
            CheckCanCraft();
        }
    }

    private bool CheckCanCraft() {
        Debug.Log(thisKeyRecipe);
        foreach (string ingredient in thisKeyRecipe) {
            // Stop early if the recipe has less than 4 ingredients
            if (ingredient == noIngredient) {
                break;
            }
            foreach (GameObject obj in ingredientObjs) {
                if ((obj.GetComponent<MaterialValue>().GetMatName() == ingredient) && (GetFloatVariable(obj.name) > 0)) {
                    // Debug.Log("returned false from CanCheckCraft()");   // TODO: remove
                    return false;
                }
            }
        }
        // Debug.Log("returned true from CanCheckCraft()"); // TODO: remove
        return true;
    } 

    private void SetAbleToCraft() {
        SetColor(true);
    }

    public void SetCraftedStatus(bool status) {
        checkmark.enabled = status;
    }
}
