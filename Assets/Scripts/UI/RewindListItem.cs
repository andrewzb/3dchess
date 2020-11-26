using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewindListItem : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI indexLabel;
    [SerializeField]
    public TextMeshProUGUI actionDescription;

    private bool IsSelected { get; set; } = false;
    private int Index { get; set; }
    private BoardCellId From { get; set; }
    private BoardCellId To { get; set; }

    public void Init()
    {
        indexLabel.text = $"Turn: {Index}";
        actionDescription.text = $"F: {From.ToString()}, T: {To.ToString()}";
    }

    public RewindListItem(int index, BoardCellId from, BoardCellId to, bool isSelected = false)
    {
        Index = index;
        From = from;
        To = to;
        IsSelected = isSelected;
    }

    public RewindListItem FluentResetValues(int index, BoardCellId from, BoardCellId to, bool isSelected = false)
    {
        Index = index;
        From = from;
        To = to;
        IsSelected = isSelected;
        return this;
    }
}
