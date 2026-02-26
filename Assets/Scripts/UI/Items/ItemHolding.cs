using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolding : MonoBehaviour
{
    public GameObject InventoryObject;
    private Animator inventoryAnimator;

    public Image FirstSlotBackground;
    public Image FirstSlotItem;

    public Image SecondSlotBackground;
    public Image SecondSlotItem;

    public Image ThirdSlotBackground;
    public Image ThirdSlotItem;

    public Sprite BlankBackGround;
    public Sprite HighlightedBackground;
    public Sprite TransparentItem;
    public Sprite ItemONE;
    public Sprite ItemTWO;
    public Sprite ItemTHREE;

    public static bool HasFirstItem;
    public static bool HasSecondItem;
    public static bool HasThirdItem;

    private void Start()
    {
        HasFirstItem = false; HasSecondItem = false; HasThirdItem = false;
        inventoryAnimator = InventoryObject.GetComponent<Animator>(); // Find the game objects animation
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(OpenInventory());
        }


        // SLOT 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            FirstSlotBackground.sprite = HighlightedBackground;
            SecondSlotBackground.sprite = BlankBackGround;
            ThirdSlotBackground.sprite = BlankBackGround;
        }

        // SLOT 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FirstSlotBackground.sprite = BlankBackGround;
            SecondSlotBackground.sprite = HighlightedBackground;
            ThirdSlotBackground.sprite = BlankBackGround;
        }

        // SLOT 3
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FirstSlotBackground.sprite = BlankBackGround;
            SecondSlotBackground.sprite = BlankBackGround;
            ThirdSlotBackground.sprite = HighlightedBackground;
        }

        // If they have the first item
        if (HasFirstItem)
            FirstSlotItem.sprite = ItemONE;
        if (!HasFirstItem)
            FirstSlotItem.sprite = TransparentItem;

        // If they have the second item
        if (HasSecondItem)
            SecondSlotItem.sprite = ItemTWO;
        if (!HasSecondItem)
            SecondSlotItem.sprite = TransparentItem;

        // If they have the third item
        if (HasThirdItem)
            ThirdSlotItem.sprite = ItemTHREE;
        if (!HasThirdItem)
            ThirdSlotItem.sprite = TransparentItem;

    }

    IEnumerator OpenInventory()
    {
        inventoryAnimator.SetBool("On", true);

        yield return new WaitForSeconds(0.7f);

        inventoryAnimator.SetBool("On", false);
    }
}

