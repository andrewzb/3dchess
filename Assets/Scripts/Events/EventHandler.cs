using System;
using System.Collections.Generic;

public delegate void DelegatExaple();

public static class EventHandler
{
    //public static event Action<InventoryLocation, List<InventoryItem>> InventoryUpdatedEvent;
    /*public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (InventoryUpdatedEvent != null)
            InventoryUpdatedEvent(inventoryLocation, inventoryList);
    }*/

    // MovementEvent
    public static event DelegatExaple DelegatExapleEvent;
    // MovementEvent Call for Publishers

    public static void CallDelegatExapleEvent()
    {
        if (DelegatExapleEvent != null)
        {
            DelegatExapleEvent();
        }
    }


}