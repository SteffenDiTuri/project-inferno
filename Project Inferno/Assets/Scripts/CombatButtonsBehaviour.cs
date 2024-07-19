using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatButtonsBehaviour : MonoBehaviour
{
    public GameObject attacksSelection;
    public GameObject abilitiesSelection;
    public GameObject itemsSelection;
    public GameObject backgroundFade;
    
    // Start is called before the first frame update
    void Start()
    {
        attacksSelection.SetActive(false);
        abilitiesSelection.SetActive(false);
        itemsSelection.SetActive(false);

        backgroundFade.SetActive(false);
    }

    // showing sections
    public void ShowAttacksSelection(){
        attacksSelection.SetActive(true);
        SectionClicked();
    }

    public void ShowAbilitiesSelection(){
        abilitiesSelection.SetActive(true);
        SectionClicked();
    }

    public void ShowItemsSelection(){
        itemsSelection.SetActive(true);
        SectionClicked();
    }

    // closing sections
    public void CloseAttacksSelection(){
        attacksSelection.SetActive(false);
        ActionClicked();
    }

    public void CloseAbilitiesSelection(){
        abilitiesSelection.SetActive(false);
        ActionClicked();
    }

    public void CloseItemsSelection(){
        itemsSelection.SetActive(false);
        ActionClicked();
    }

    // helper methods
    public void SectionClicked(){
        backgroundFade.SetActive(true);
    }

    public void ActionClicked(){
        backgroundFade.SetActive(false);

        // close all open sections (just to be sure)
        attacksSelection.SetActive(false);
        abilitiesSelection.SetActive(false);
        itemsSelection.SetActive(false);
    }
}
