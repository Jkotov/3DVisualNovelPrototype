using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text;

public class ItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int buttonID;
    public Item thisItem;


    public Tooltips tooltip;

    public Item GetThisItem()
    {
        for(int i = 0; i < GameManager.instance.items.Count; i++)
        {
            if(buttonID == i)
            {
                thisItem = GameManager.instance.items[i];
            }
        }
        return thisItem;
    }

    public void CloseButton()
    {
        GameManager.instance.RemoveItem(GetThisItem());

        thisItem = GetThisItem();
        if(thisItem != null)
        {
            tooltip.ShowTooltip();
            tooltip.UpdateTooltip(GetDetailText(thisItem));
        }
        else
        {
            tooltip.HideTooltip();
            tooltip.UpdateTooltip("");
        }
    }


    public void CraftItem()
    {
        CraftButton name = FindObjectOfType<CraftButton>();
        GameManager.instance.DisplayCraftItem(thisItem);
        GameManager.instance.DisplayCraftItems(thisItem.itemsss[0], thisItem.itemsss[1]);
        //name.SetItem(thisItem);
        name.SetButton(this);
    }

    public void Boileritem()
    {
        BoilerButton name = FindObjectOfType<BoilerButton>();
        BoilerButton1 name1 = FindObjectOfType<BoilerButton1>();
        BoilerButton2 name2 = FindObjectOfType<BoilerButton2>();
        if (name.ItemButton == null)
        {
            GameManager.instance.DisplayBoilerItem_1(thisItem);
            //GameManager.instance.RemoveItem(name.ItemButton.GetThisItem());
            name.SetButtonBoiler(this);
        }
        else if(name1.ItemButton == null)
        {
            GameManager.instance.DisplayBoilerItem_2(thisItem);
            name1.SetButtonBoiler1(this);

        }
        else if (name2.ItemButton == null && name.ItemButton != null)
        {
            GameManager.instance.DisplayBoilerItem_3(thisItem);
            name2.SetButtonBoiler2(this);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetThisItem();
        if(thisItem != null)
        {
            Debug.Log("Enter " + thisItem.itemName + " Slot");

            tooltip.ShowTooltip();
            tooltip.UpdateTooltip(GetDetailText(thisItem));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
            tooltip.HideTooltip();
            tooltip.UpdateTooltip("");
    }

    public string GetDetailText(Item _item)
    {
        if(_item == null)
        {
            return "";
        }
        else
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Предмет: {0}\n\n", _item.itemName);
            stringBuilder.AppendFormat("Цена вопроса: {0}\n\n" +
                                        "Описание: {1}\n\n", _item.itemPrice, _item.itemDes);

            return stringBuilder.ToString();
        }
    }
}
