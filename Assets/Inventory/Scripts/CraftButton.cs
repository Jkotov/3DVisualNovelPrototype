using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public int button_ID;
    [SerializeField]
    private Item thisItem;
    public ItemButton ItemButton;

    public Tooltips tooltip;

    public void SetItem(Item _item)
    {
        thisItem = _item;
    }
    public void SetButton(ItemButton _button)
    {
        ItemButton = _button;
        thisItem = ItemButton.thisItem;
    }

    public void ClickCraft()
    {
        if(thisItem != null)
        {
            //thisItem = ItemButton.GetThisItem();
            GameManager.instance.AddItem(thisItem.itemsss[0]);
            GameManager.instance.AddItem(thisItem.itemsss[1]);
            tooltip.ShowTooltip();
            GameManager.instance.RemoveItem(ItemButton.GetThisItem());
        }
    }


    public void Clean()
    {
        ItemButton = null;
        thisItem = null;
    }
}
