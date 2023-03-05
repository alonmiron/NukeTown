using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer1 : MonoBehaviour
{
   public Animator drawer1;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      { 
         drawer1.SetTrigger("opendrawer1");
      }
   }

   public void OnOpen()
   {
      drawer1.SetTrigger("opendrawer1");
   
   }
}
