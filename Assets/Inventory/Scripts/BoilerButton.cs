using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilerButton : MonoBehaviour
{
    [SerializeField]
    private Item thisItem;
    public ItemButton ItemButton;

    public Tooltips tooltip;

    public void SetButtonBoiler(ItemButton _button)
    {
        ItemButton = _button;
        thisItem = ItemButton.thisItem;
        BoilerCraft craft = FindObjectOfType<BoilerCraft>();
        craft.SetButtonCraft(ItemButton);
    }


    public void Clean()
    {
        ItemButton = null;
        thisItem = null;
    }
}
