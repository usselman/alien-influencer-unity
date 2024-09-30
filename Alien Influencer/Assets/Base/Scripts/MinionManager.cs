using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : Singleton<MinionManager>
{
    public GameObject SelectedBuilding;
    public static event System.Action OnNewBuildingSelected;
    public void NewBuildingSelection(GameObject newSelectionBuilding)
    {
        SelectedBuilding = newSelectionBuilding;
        OnNewBuildingSelected?.Invoke();
    }
    public void AttackBuilding()
    {
        SelectedBuilding.GetComponent<Building>()?.AddDamage(10);
    }
}
