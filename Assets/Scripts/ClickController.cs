﻿using UnityEngine;
using System.Collections.Generic;

public class ClickController : MonoBehaviour {

    public int clickableLayer;
    public Selection currentSelection;

    private Selection[] currentSelections;
	
	void Update ()
    {
	    if(Input.GetButtonUp("Mouse Primary"))
        {
            GetSelection();
            if (currentSelections != null && currentSelections.Length > 0)
            {
                ChooseCurrentSelection();
                if (currentSelection.GetComponentInParent<BuildingController>())
                currentSelection.GetComponentInParent<BuildingController>().setTeam(Random.Range(0, currentSelection.GetComponentInParent<BuildingController>().teamSprites.Length));
            }
        }

	}

    void GetSelection ()
    {
        int mask = 1 << clickableLayer;

        List<Selection> crnt = new List<Selection>();

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);

        if (hits == null || hits.Length == 0)
        {
            currentSelections = null;
        }
        else
        {
            foreach (RaycastHit2D hit in hits)
            {
                Selection select = hit.transform.GetComponentInChildren<Selection>();
                if (select)
                {
                    crnt.Add(select);
                }
                
            }
            currentSelections = crnt.ToArray();
        }
        
    }

    void ChooseCurrentSelection ()
    {
        
        int max = int.MaxValue;
        foreach (Selection select in currentSelections)
        {
            if (select.priority < max)
            {
                currentSelection = select;
                max = currentSelection.priority;
            }
        }
    }

}