using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilerButton1 : MonoBehaviour
{
    [SerializeField]
    private Item thisItem;
    public ItemButton ItemButton;

    public Tooltips tooltip;

    public void SetButtonBoiler1(ItemButton _button)
    {
        ItemButton = _button;
        thisItem = ItemButton.thisItem;
        BoilerCraft craft = FindObjectOfType<BoilerCraft>();
        craft.SetButtonCraft1(ItemButton);
    }


    public void Clean()
    {
        ItemButton = null;
        thisItem = null;
    }
}
