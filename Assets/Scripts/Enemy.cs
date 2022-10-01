using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private TextMesh text;
    #endregion

    #region Methods
    public void ReceiveAttack(int powerLevel, Vector3 direction)
    {
        text.text = powerLevel.ToString();

        if (powerLevel > 0)
        {
            text.transform.DOKill();
            text.transform.DOPunchScale(Vector3.one * 3f, 0.3f);
            Debug.Log($"Hit - {powerLevel}");
        }
    }
    #endregion
}
