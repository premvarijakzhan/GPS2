using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawTrails : MonoBehaviour
{
    public static DrawTrails DT = null;

    public GameObject swipePrefab;
    public bool mouseInteraction = true;
    public Camera cam;

    GameObject thisTrail;
    Vector3 screenPos;
    Vector3 drawPos;
    TriggerDetection[] triggers;

    void Awake()
    {
        if (DT == null)
        {
            DT = this;
            DontDestroyOnLoad(this);
        }
        else if (DT != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        triggers = FindObjectsOfType<TriggerDetection>();
    }

    void Update()
    {
        if (IsMouseOverUI())
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            {
                triggers = FindObjectsOfType<TriggerDetection>();
                screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                drawPos = cam.ScreenToWorldPoint(screenPos);

                thisTrail = (GameObject)Instantiate(swipePrefab, drawPos, Quaternion.identity);
            }
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0))
            {
                screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                drawPos = cam.ScreenToWorldPoint(screenPos);

                if (thisTrail != null)
                    thisTrail.transform.position = drawPos;
            }
            else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0))
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].CorrectSymbol())
                    {
                        Destroy(SymbolManager.SM.symbol);
                        break;
                    }
                }

                DestroyTrail();
            }
        }
        else
        {
            DestroyTrail();
        }
    }

    void DestroyTrail()
    {
        Destroy(thisTrail);

        SymbolManager.SM.triggerCount = 0;

        for (int i = 0; i < triggers.Length; i++)
        {
            triggers[i].isFirstNode = false;
            triggers[i].isLastNode = false;
            triggers[i].alreadyTriggered = false;
            triggers[i].isLastNodeTriggered = false;
        }
    }

    bool IsMouseOverUI()
    {
        if (mouseInteraction)
            return EventSystem.current.IsPointerOverGameObject();
        else
            return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
    }
}
