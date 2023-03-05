using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
   public Animator drawer;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      { 
         drawer.SetTrigger("opendrawer");
      }
   }

   public void OnOven()
   {
      drawer.SetTrigger("opendrawer");
   
   }
}
