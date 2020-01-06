using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformFinder
{
    //Metodos para encontrar objetos que hereda de un objetos utilizando backtraking
    public static Transform DeepFind(this Transform parent, string targetName) {

        Transform tempTrans = null;

        foreach(Transform child in parent) {
            if(child.name == targetName) {
                return child;
            }
            else {
                tempTrans = DeepFind(child, targetName);
                if(tempTrans != null) {
                    return tempTrans;
                }
            }
        }
        return null;
    }

}
