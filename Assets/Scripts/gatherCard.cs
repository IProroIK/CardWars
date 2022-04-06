using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class gatherCard : MonoBehaviour
{
    public static event Action OnLastCardReached;

    public Transform stack;

    [SerializeField]private WrongCard _wrongCard;

    [SerializeField] private int _amountOfCard = 16;
    [SerializeField] private int _previousID = 1;
    [SerializeField] private int _currentID;
    [SerializeField] private int _nextCardID = 1;
    private CardInfo _cardInfo;
    private Transform _card;
    private Transform _previousCard = null;

    private void Start()
    {
        _wrongCard = GetComponent<WrongCard>();
    }

    // Update is called once per frame
    private void Update()
    {
    
        if(Input.GetMouseButton(0))
        {
            checkId();
        }
    }

    private void checkId()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, LayerMask.GetMask("Card")))
        {
            

            _card = hit.transform;
            
            _cardInfo = _card.GetComponent<CardInfo>();
            _wrongCard = _card.GetComponent<WrongCard>();
            _currentID = _cardInfo.cardID;
            if( (_card != _previousCard && _previousCard != null) && (_previousID + 1 == _currentID) && !DOTween.IsTweening(_card)&&(_currentID == _nextCardID+1))
            {
                pickCards(_previousCard, _card, stack);
                _nextCardID += 1;
                if(_nextCardID == _amountOfCard)
                {
                    OnLastCardReached?.Invoke();
                }
            }
            else if((_card != _previousCard && _previousCard != null) && (_previousID + 1 != _currentID) && !DOTween.IsTweening(_card)&&(_currentID != _nextCardID))
            {
                _wrongCard.changeColor();
                if (!DOTween.IsTweening(_previousCard))
                {
                    _wrongCard.punch(_previousCard, _card);
                }
            }
            _previousCard = _card;
            _previousID = _currentID;
            
        }
    }
    public void pickCards(Transform firstCard, Transform secondCard, Transform stack)
    {
        stack.DOMove(secondCard.position, 0.5f, false);
        firstCard.SetParent(stack);
        firstCard.DOLocalMove(new Vector3(0f, 0f, 0f), 0.5f, false);//add some transform to x and z axis
        secondCard.DOMoveY(secondCard.position.y + 0.3f, 0.5f, false);
    }


}
