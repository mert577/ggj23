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


  public static void TimeStop(float duration){
    Time.timeScale = 0f;
    DOTween.To(()=>Time.timeScale, x=>Time.timeScale = x, 1f, duration).SetEase(Ease.OutCubic);
  }
}
