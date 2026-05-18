using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFunctionJumpHandler
{
    public void Jump(int functionJumpId, List<string> paramArray);
}