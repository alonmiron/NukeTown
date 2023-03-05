using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer2 : MonoBehaviour
{
   public Animator drawer2;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      { 
         drawer2.SetTrigger("opendrawer2");
      }
   }

   public void OnOpen()
   {
      drawer2.SetTrigger("opendrawer2");
   
   }
}
