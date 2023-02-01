using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilerCraft : MonoBehaviour
{
    public Item ZelieSpeed;
    public Item ZelieSleep;
    public Item ZelieHp;
    public Item ZelieWarm;
    public ItemButton ItemButton;
    public ItemButton ItemButton1;
    public ItemButton ItemButton2;

    public Tooltips tooltip;

    public void SetButtonCraft(ItemButton _button)
    {
        ItemButton = _button;
    }
    public void SetButtonCraft1(ItemButton _button)
    {
        ItemButton1 = _button;
    }
    public void SetButtonCraft2(ItemButton _button)
    {
        ItemButton2 = _button;
    }

    public void Boiler()
    {
        if (ItemButton.thisItem == ZelieHp.itemBoiler[0] && ItemButton1.thisItem == ZelieHp.itemBoiler[1] && ItemButton2.thisItem == ZelieHp.itemBoiler[2])
        {
            GameManager.instance.AddItem(ZelieHp);
            //GameManager.instance.DisplayFinishBoiler(ZelieHp);
            GameManager.instance.RemoveBoilerItem(ItemButton.GetThisItem(), ItemButton1.GetThisItem(), ItemButton2.GetThisItem());
        }
        else if(ItemButton.thisItem == ZelieSpeed.itemBoiler[0] && ItemButton1.thisItem == ZelieSpeed.itemBoiler[1] && ItemButton2.thisItem == ZelieSpeed.itemBoiler[2])
        {
            //GameManager.instance.DisplayFinishBoiler(ZelieSpeed);
            GameManager.instance.AddItem(ZelieSpeed);

            GameManager.instance.RemoveItem(ItemButton.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton1.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton2.GetThisItem());


        }
        else if (ItemButton.thisItem == ZelieSleep.itemBoiler[0] && ItemButton1.thisItem == ZelieSleep.itemBoiler[1] && ItemButton2.thisItem == ZelieSleep.itemBoiler[2])
        {

            //GameManager.instance.DisplayFinishBoiler(ZelieSleep);
            GameManager.instance.AddItem(ZelieSleep);

            GameManager.instance.RemoveItem(ItemButton.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton1.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton2.GetThisItem());
        }
        else if (ItemButton.thisItem == ZelieWarm.itemBoiler[0] && ItemButton1.thisItem == ZelieWarm.itemBoiler[1] && ItemButton2.thisItem == ZelieWarm.itemBoiler[2])
        {
            //GameManager.instance.DisplayFinishBoiler(ZelieWarm); 
            GameManager.instance.AddItem(ZelieWarm);

            GameManager.instance.RemoveItem(ItemButton.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton1.GetThisItem());
            GameManager.instance.RemoveItem(ItemButton2.GetThisItem());
        }
    }

    public void Clean()
    {
        ItemButton = null;
    }
}
