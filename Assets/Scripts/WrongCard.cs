using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WrongCard : MonoBehaviour
{

    [SerializeField]private Material _wrongCardMatirial;
    [SerializeField] private Material _defaultMatirial;
    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
        _defaultMatirial = _meshRenderer.material;
    }

    public void changeColor()
    {
        StartCoroutine(changeColorDelay());
    }

    private IEnumerator changeColorDelay()
    {
        _meshRenderer.material = _wrongCardMatirial;
        yield return new WaitForSeconds(0.5f);
        _meshRenderer.material = _defaultMatirial;
    }

    public void punch(Transform currentCard ,Transform wrongCard)
    {
        Vector3 direction = wrongCard.position - currentCard.position;
        currentCard.DOPunchPosition(direction, 0.5f, 0, 0, false);
    }
}
