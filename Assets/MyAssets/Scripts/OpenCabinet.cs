using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCabinet : MonoBehaviour
{
   public Animator cabinet;

   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      { 
         cabinet.SetTrigger("opencabinet");
      }
   }

   public void OnOpen()
   {
      cabinet.SetTrigger("opencabinet");
   
   }
}
