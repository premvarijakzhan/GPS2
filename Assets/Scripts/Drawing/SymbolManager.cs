using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SymbolTag
{
    Square,
    Triangle,
    Arrow
}

public enum SymbolFunction
{ 
    Right,
    Left,
    Jump
}

[System.Serializable]
public class SymbolType
{
    public SymbolTag tag;
    public SymbolFunction function;
    public GameObject symbol;
    public int nodes;
    public bool isClosable;
}

public class SymbolManager : MonoBehaviour
{
    public static SymbolManager SM = null;

    public List<SymbolType> symbolType;
    public GameObject symbol;
    public GameObject symbol1;
    public GameObject symbol2;
    public int triggerCount;

    public bool isComplete = false;
    public bool turnRight = false;
    public bool turnLeft = false;
    public bool canJump = false;

    void Awake()
    {
        if (SM == null)
        {
            SM = this;
            DontDestroyOnLoad(this);
        }
        else if (SM != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void SpawnSymbol(SymbolTag tag)
    {
        for (int i = 0; i < symbolType.Count; i++)
        {
            if (symbolType[i].tag == tag)
            {
                symbol = Instantiate(symbolType[i].symbol, transform.position, Quaternion.identity);
                symbol.name = symbolType[i].symbol.name;
                symbol.transform.SetParent(transform);
            }
        }
    }

    public void SpawnSymbols(SymbolTag tag1, SymbolTag tag2)
    {
        for (int i = 0; i < symbolType.Count; i++)
        {
            if (symbolType[i].tag == tag1)
            {
                symbol1 = Instantiate(symbolType[i].symbol, new Vector3(2f, -200f, -5f), Quaternion.identity);
                symbol1.name = symbolType[i].symbol.name;
                symbol1.transform.SetParent(transform);
            }

            if (symbolType[i].tag == tag2)
            {
                symbol2 = Instantiate(symbolType[i].symbol, new Vector3(-2f, -200f, -5f), Quaternion.identity);
                symbol2.name = symbolType[i].symbol.name;
                symbol2.transform.SetParent(transform);
            }
        }
    }

    public void DoFunction(SymbolFunction function)
    {
        for (int i = 0; i < symbolType.Count; i++)
        {
            if (symbolType[i].function == function)
            {
                switch (symbolType[i].function)
                {
                    case SymbolFunction.Right:
                        Debug.Log("Right");
                        turnRight = true;
                        break;

                    case SymbolFunction.Left:
                        Debug.Log("Left");
                        turnLeft = true;
                        break;

                    case SymbolFunction.Jump:
                        Debug.Log("Jump");
                        canJump = true;
                        break;
                }
            }
        }
    }

    public void CheckSymbol()
    {
        foreach (SymbolType st in symbolType)
        {
            if (symbol != null && symbol.name == st.symbol.name)
            {
                if (triggerCount == st.nodes)
                {
                    triggerCount = st.nodes;
                    DoFunction(st.function);
                    Destroy(symbol);
                }

                break;
            }

            if (symbol1 != null && symbol1.name == st.symbol.name)
            {
                if (isComplete && triggerCount == 5)
                {
                    DoFunction(st.function);
                    Destroy(symbol1);
                    Destroy(symbol2);
                }

                break;
            }

            if (symbol2 != null && symbol2.name == st.symbol.name)
            {
                if (isComplete && triggerCount == 4)
                {
                    DoFunction(st.function);
                    Destroy(symbol1);
                    Destroy(symbol2);
                }
            }
        }
    }
}
