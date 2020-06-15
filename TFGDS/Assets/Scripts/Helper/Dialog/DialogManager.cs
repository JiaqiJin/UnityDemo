using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogMachineUI machine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown("1"))
        {
            machine.StartDialog();
        }*/
        if (Input.GetKeyDown("2"))
        {
            machine.UserClicked();
        }
    }

    public void StartDialog(Story01 data, DialogConfig assert)
    {
        machine.data = data;
        machine.assert = assert;
        machine.StartDialog();
    }

}
