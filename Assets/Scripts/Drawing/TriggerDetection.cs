﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    public bool isFirstNode = false;
    public bool isLastNode = false;
    public bool alreadyTriggered = false;
    public bool isLastNodeTriggered = false;

    void Update()
    {
        if (isFirstNode)
            isLastNode = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Swipe"))
        {
            if (!alreadyTriggered && !SymbolManager.SM.isComplete)
            {
                SymbolManager.SM.triggerCount++;
                alreadyTriggered = true;
            }

            foreach (SymbolType st in SymbolManager.SM.symbolType)
            {
                if (st.isClosable)
                {
                    if (SymbolManager.SM.triggerCount == (st.nodes - 1) && isLastNode && !isLastNodeTriggered)
                    {
                        SymbolManager.SM.triggerCount++;
                        isLastNodeTriggered = true;
                        SymbolManager.SM.isComplete = true;
                    }

                    if (SymbolManager.SM.triggerCount == 1)
                        isFirstNode = true;
                    else
                        isFirstNode = false;
                }
            }
        }
    }
}
