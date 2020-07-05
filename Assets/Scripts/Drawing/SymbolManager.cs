using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SymbolTag
{
    Square,
    Triangle
}

public enum SymbolFunction
{ 
    Right,
    Left
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
    public int triggerCount;

    public bool isSquare;
    Transform player;
    TiltMovement tm;

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

    void Start()
    {
        if (isSquare)
            SpawnSymbol(SymbolTag.Square);
        else
            SpawnSymbol(SymbolTag.Triangle);

        player = GameObject.Find("Player").transform;
        tm = GameObject.Find("Player").GetComponent<TiltMovement>();
    }

    public void SpawnSymbol(SymbolTag tag)
    {
        for (int i = 0; i < symbolType.Count; i++)
        {
            if (symbolType[i].tag == tag)
            {
                symbol = Instantiate(symbolType[i].symbol, transform.position, Quaternion.identity);
                symbol.name = symbolType[i].symbol.name;
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
                        tm.Jump();
                        break;

                    case SymbolFunction.Left:
                        Debug.Log("Left");
                        //function
                        break;
                }
            }
        }
    }
}
