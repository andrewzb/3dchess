using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing;

    Button expandButton;
    SettingsMenuItem[] menuItems;
    private bool isExpanded = false;
    Vector2 expandButtonPosition;
    int itemsCount;


    // Start is called before the first frame update
    void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new SettingsMenuItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent<SettingsMenuItem>();
        }
        expandButton = transform.GetChild(0).GetComponent<Button>();
        expandButton.onClick.AddListener(ToggleMenu);
        expandButton.transform.SetAsLastSibling();
        expandButtonPosition = expandButton.transform.position;

        ResetPosition();
    }

    private void ResetPosition()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].position.position = expandButtonPosition;
        }
    }

    private void ToggleMenu()
    {
        isExpanded = !isExpanded;
        if(isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].position.position = expandButtonPosition + spacing * (i + 1);
            }
        }
        else
        {
            for (int i = 0; i < itemsCount; i++)
            {
                menuItems[i].position.position = expandButtonPosition;
            }
        }
    }

    private void OnDestroy()
    {
        expandButton.onClick.RemoveListener(ToggleMenu);
    }
}
