using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPaused;

    public GameObject inventoryWindow;

    public List<Item> items = new List<Item>(); //какие предметы мы имеем
    public List<int> itemNumbers = new List<int>(); //сколько мы имеем
    public GameObject[] slots;

    public GameObject scissorsslot;
    public GameObject scissorsslot1;
    public GameObject scissorsslot2;

    public GameObject startBoiler_1;
    public GameObject startBoiler_2;
    public GameObject startBoiler_3;

    public GameObject FinishBoiler;

    public GameObject finalBoiler;

    //public Dictionary<Item, int> itemDict = new Dictionary<Item, int>();
    public ItemButton thisButton;
    public ItemButton[] itemButtons;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else{ 
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        DisplayItems();
    }

    private void Update()
    {
        
    }


    private void DisplayItems()
    {
        #region
        //for (int i = 0; i < items.Count; i++)
        //{
        //    slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        //    slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;

        //    slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
        //    slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumbers[i].ToString();

        //    slots[i].transform.GetChild(2).gameObject.SetActive(true);
        //}
        #endregion

        for(int i = 0; i < slots.Length; i++)
        {
            if(i < items.Count)
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemSprite;

                slots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);

                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(1).GetComponent<Text>().text = itemNumbers[i].ToString();

                slots[i].transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                slots[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                slots[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

                slots[i].transform.GetChild(1).GetComponent<Text>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(1).GetComponent<Text>().text = null;

                slots[i].transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void DisplayCraftItems(Item _item, Item _item2)
    {
        scissorsslot1.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        scissorsslot1.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;

        scissorsslot2.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        scissorsslot2.transform.GetChild(0).GetComponent<Image>().sprite = _item2.itemSprite;
    }

    public void DestroyCraftItems()
    {
        scissorsslot1.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        scissorsslot1.transform.GetChild(0).GetComponent<Image>().sprite = null;

        scissorsslot2.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        scissorsslot2.transform.GetChild(0).GetComponent<Image>().sprite = null;

        scissorsslot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        scissorsslot.transform.GetChild(0).GetComponent<Image>().sprite = null;
    }

    public void DisplayCraftItem(Item _item)
    {
            scissorsslot.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            scissorsslot.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;
            //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DisplayBoilerItem_1(Item _item)
    {
        startBoiler_1.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        startBoiler_1.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;
        //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DisplayBoilerItem_2(Item _item)
    {
        startBoiler_2.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        startBoiler_2.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;
        //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void DisplayBoilerItem_3(Item _item)
    {
        startBoiler_3.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
        startBoiler_3.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;
        //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);
    }

    //public void DisplayFinishBoiler(Item _item)
    //{
    //    FinishBoiler.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
    //    FinishBoiler.transform.GetChild(0).GetComponent<Image>().sprite = _item.itemSprite;
    //    //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);
    //}

    public void DestroyBoilerItems()
    {
        startBoiler_1.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        startBoiler_1.transform.GetChild(0).GetComponent<Image>().sprite = null;
        //scissorsslot.transform.GetChild(1).gameObject.SetActive(true);

        startBoiler_2.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        startBoiler_2.transform.GetChild(0).GetComponent<Image>().sprite = null;

        startBoiler_3.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        startBoiler_3.transform.GetChild(0).GetComponent<Image>().sprite = null;

        //FinishBoiler.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
        //FinishBoiler.transform.GetChild(0).GetComponent<Image>().sprite = null;

        FindObjectOfType<BoilerButton>().Clean();
        FindObjectOfType<BoilerButton1>().Clean();
        FindObjectOfType<BoilerButton2>().Clean();

    }

    public bool FindItem(Item _item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (_item == items[i])
            {
                return true;
            }
        }
        return false;
    }

    public void AddItem(Item _item)
    {
        // если предмет уже существует в инвентаре
        if (!items.Contains(_item))
        {
            items.Add(_item);
            itemNumbers.Add(1);
        }
        //если предмет новенький
        else
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(_item == items[i])
                {
                    itemNumbers[i]++;
                }
            }
        }
        DisplayItems();
    }

    public void RemoveBoilerItem(Item _item, Item _item1, Item _item2)
    {
        if (items.Contains(_item) && items.Contains(_item1) && items.Contains(_item2))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (_item == items[i])
                {
                    itemNumbers[i]--;
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(_item);
                        itemNumbers.Remove(itemNumbers[i]); 
                        if (startBoiler_1.transform.childCount != 0 || startBoiler_2.transform.childCount != 0 || startBoiler_3.transform.childCount != 0)
                        {
                            DestroyBoilerItems();
                            FindObjectOfType<BoilerButton>().Clean();
                            FindObjectOfType<BoilerButton1>().Clean();
                            FindObjectOfType<BoilerButton2>().Clean();
                        }
                    }
                }
                else if (_item1 == items[i])
                {
                    itemNumbers[i]--;
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(_item1);
                        itemNumbers.Remove(itemNumbers[i]); 
                        if (startBoiler_1.transform.childCount != 0 || startBoiler_2.transform.childCount != 0 || startBoiler_3.transform.childCount != 0)
                        {
                            DestroyBoilerItems();
                            FindObjectOfType<BoilerButton>().Clean();
                            FindObjectOfType<BoilerButton1>().Clean();
                            FindObjectOfType<BoilerButton2>().Clean();
                        }
                    }
                }
                else if (_item2 == items[i])
                {
                    itemNumbers[i]--;
                    if (itemNumbers[i] == 0)
                    {
                        items.Remove(_item2);
                        itemNumbers.Remove(itemNumbers[i]);
                        if (startBoiler_1.transform.childCount != 0 || startBoiler_2.transform.childCount != 0 || startBoiler_3.transform.childCount != 0)
                        {
                            DestroyBoilerItems();
                            FindObjectOfType<BoilerButton>().Clean();
                            FindObjectOfType<BoilerButton1>().Clean();
                            FindObjectOfType<BoilerButton2>().Clean();
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("There is no " + _item + " in my bag");
        }

        ResetButtonItems();
        DisplayItems();
    }

    public void RemoveItem(Item _item)
    {
        if (items.Contains(_item))
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(_item == items[i])
                {
                    itemNumbers[i]--;
                    if(itemNumbers[i] == 0)
                    {
                        items.Remove(_item);
                        itemNumbers.Remove(itemNumbers[i]);
                        
                        if (scissorsslot.transform.childCount != 0 && inventoryWindow.activeSelf)
                        {
                            DestroyCraftItems();
                            FindObjectOfType<CraftButton>().Clean();
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("There is no " + _item + " in my bag");
        }

        ResetButtonItems();
        DisplayItems();
    }


    public void ResetButtonItems()
    {
        for(int i = 0; i < itemButtons.Length; i++)
        {
            if(i < items.Count)
            {
                itemButtons[i].thisItem = items[i];
            }
            else
            {
                itemButtons[i].thisItem = null;
            }
        }
    }
}
