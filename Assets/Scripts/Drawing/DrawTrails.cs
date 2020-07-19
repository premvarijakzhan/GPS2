using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTrails : MonoBehaviour
{
    public GameObject swipePrefab;
    public Camera cam;

    GameObject thisTrail;
    Vector3 screenPos;
    Vector3 drawPos;
    TriggerDetection[] triggers;

    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) || Input.GetMouseButtonDown(0))
        {
            screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
            drawPos = cam.ScreenToWorldPoint(screenPos);

            thisTrail = (GameObject)Instantiate(swipePrefab, drawPos, Quaternion.identity);
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) || Input.GetMouseButton(0))
        {
            screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
            drawPos = cam.ScreenToWorldPoint(screenPos);

            if (thisTrail != null)
                thisTrail.transform.position = drawPos;
        }
        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
        {
            CheckSymbol();
        }
    }

    public void CheckSymbol()
    {
        foreach (SymbolType st in SymbolManager.SM.symbolType)
        {
            if (SymbolManager.SM.symbol != null && SymbolManager.SM.symbol.name == st.symbol.name)
            {
                if (SymbolManager.SM.triggerCount == st.nodes)
                {
                    SymbolManager.SM.triggerCount = st.nodes;
                    SymbolManager.SM.DoFunction(st.function);
                    Destroy(SymbolManager.SM.symbol);
                }

                DestroyTrail();
            }
        }
    }

    void DestroyTrail()
    {
        Destroy(thisTrail);
        SymbolManager.SM.triggerCount = 0;

        triggers = FindObjectsOfType<TriggerDetection>();

        foreach (TriggerDetection td in triggers)
        {
            td.isFirstNode = false;
            td.isLastNode = false;
            td.alreadyTriggered = false;
            td.isLastNodeTriggered = false;
        }

    }
}
