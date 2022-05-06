using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
  /* This script is still a work in progress */
  
    //Colorblind colorblindScript;

    public void HandleInputData(int value)
    {
        if(value == 0){
           // new GUIContent("Normal Vision"),
           // bind the 'Type' parameter of the Colorblind script to dropdown in GUI
          //  colorblindScript.Type = value;
        }
        if(value == 1){
          // new GUIContent("Protanopia"),
          // bind the 'Type' parameter of the Colorblind script to dropdown in GUI
         // colorblindScript.Type = value;
        }
        if(value == 2){
         // new GUIContent("Deuteranopia"),
           // bind the 'Type' parameter of the Colorblind script to dropdown in GUI
         // colorblindScript.Type = value;
        }
        if(value == 3){
          // new GUIContent("Tritanopia")
          // bind the 'Type' parameter of the Colorblind script to dropdown in GUI
        //  colorblindScript.Type = value;
        }
    }

}
