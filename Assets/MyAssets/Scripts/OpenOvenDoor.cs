using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOvenDoor : MonoBehaviour
{
   public Animator oven;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      { 
         oven.SetTrigger("ovendoor");
      }
   }

   public void OnOven()
   {
      oven.SetTrigger("ovendoor");
   
   }
}
