using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFieldOfViewController : MonoBehaviour
{
   [SerializeField] private float valueDefault = 60f;
   [SerializeField] private float valueZoom = 40f;
   [SerializeField] private float timerZoom;


   public void ZoomCamera()
   {
      StartCoroutine(ZoomCor(valueZoom));
   }

   public void ZoomToDefault()
   {
      StartCoroutine(ZoomCor(valueDefault));
   }

   IEnumerator ZoomCor(float endZoom)
   {
      var camera = Camera.main;
      var startZoom = camera.fieldOfView;
      float t = 0f;
      while (t < timerZoom)
      {
         camera.fieldOfView = Mathf.Lerp(startZoom, endZoom, t / timerZoom);
         t += Time.deltaTime;
         yield return null;
      }

      camera.fieldOfView = endZoom;
   }
}
