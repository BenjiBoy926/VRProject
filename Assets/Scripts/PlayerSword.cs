using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using NaughtyAttributes;

public class PlayerSword : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private XRGrabInteractable _interactable;
    [SerializeField]
    private Transform _tip;

    [SerializeField]
    private Vector2 _speedBounds = new Vector2(0.3f, 1.5f);

    [SerializeField, Foldout("Info"), ReadOnly]
    private Vector3 _previousTipPosition;
    [SerializeField, Foldout("Info"), ReadOnly]
    private Vector3 _tipVelocity;
    [SerializeField, Foldout("Info"), ReadOnly]
    private float _tipSpeed;
    #endregion

    #region Methods
    // Unity Messages ================================================================
    private void Awake()
    {
        _interactable.selectEntered.AddListener(Interactable_SelectedEntered);
        _interactable.selectExited.AddListener(Interactable_SelectedExited);
    }
    private void OnDestroy()
    {
        _interactable.selectEntered.RemoveListener(Interactable_SelectedEntered);
        _interactable.selectExited.RemoveListener(Interactable_SelectedExited);
    }
    private void FixedUpdate()
    {
        Vector3 position = _tip.position;
        
        _tipVelocity = position - _previousTipPosition;
        _tipSpeed = _tipVelocity.magnitude;

        _previousTipPosition = position;
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        Enemy enemy = rb.GetComponent<Enemy>();
        if (enemy == null) return;

        int swingStrength = GetCurrentSwingStrength();
        enemy.ReceiveAttack(swingStrength, _tipVelocity);
    }
    // Other Methods =============================================================
    private int GetCurrentSwingStrength()
    {
        float t = Mathf.InverseLerp(_speedBounds.x, _speedBounds.y, _tipSpeed);
        t = Mathf.Clamp01(t);
        return (int)(t * 4);
    }
    private void Interactable_SelectedEntered(SelectEnterEventArgs args)
    {
        enabled = true;
    }
    private void Interactable_SelectedExited(SelectExitEventArgs args)
    {
        enabled = false;
    }
    #endregion
}
