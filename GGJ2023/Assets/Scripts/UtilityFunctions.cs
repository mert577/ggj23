using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UtilityFunctions : MonoBehaviour
{

    public static Vector2 GetDirectionFromAngle(float angleDegrees){
    float angleRadians = angleDegrees * Mathf.Deg2Rad;
    return new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));
  }


  public static float GetAngleFromDirection(Vector2 direction){
    direction = direction.normalized;
    float n = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    return n;
  }


  public static Vector2 GetOrthagonalDirection(Vector2 direction){
    return new Vector2(direction.y, -direction.x);
  }


  public static float Map(float value, float inMin, float inMax, float outMin, float outMax){
    return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
  }

  
  public static void CameraShake(){
    Camera.main.transform.DORewind();
    Camera.main.transform.DOShakePosition(0.5f, 0.5f, 10, 90, false, true);
  }

  public static void TimeStop(float duration){
    Time.timeScale = 0f;
    DOTween.To(()=>Time.timeScale, x=>Time.timeScale = x, 1f, duration).SetEase(Ease.OutCubic);
  }
}
