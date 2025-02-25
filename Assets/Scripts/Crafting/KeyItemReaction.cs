using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class KeyItemReaction : ItemReaction
{
    [SerializeField] private Image checkmark;
    private Image keyImage;
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

        // Set this image
        keyImage = GetComponent<Image>();

        // Hide checkmark image
        SetCraftedStatus(false);
    }

    void OnEnable() {
        if (thisKeyRecipe != null) {
            CheckCanCraft();
        }
    }

    private bool CheckCanCraft() {
        // Looking into the recipe solution guide
        foreach (string ingredient in thisKeyRecipe) {
            // Stop early if the recipe has less than 4 ingredients
            if (ingredient == noIngredient) {
                break;
            }
            // Now comparing with all the available game objects
            foreach (GameObject obj in ingredientObjs) {
                if ((obj.GetComponent<MaterialValue>().GetMatName() == ingredient) && (GetFloatVariable(obj.name) <= 0)) {
                    return false;
                }
            }
        }
        SetAbleToCraft();
        return true;
    }

    private void SetAbleToCraft() {
        SetColor(true, keyImage);
    }

    public void SetCraftedStatus(bool status) {
        checkmark.enabled = status;
    }
}
