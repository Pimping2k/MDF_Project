using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Phase
{
    public class PhaseTurnUI : MonoBehaviour
    {
        [SerializeField] private Transform _textHolder;
        [SerializeField] private float _targetPosition;
        [SerializeField] private float _initialPosition;
        [SerializeField] private float _duration;
        
        public async void ShowParticipantTurn(bool isPlayer)
        {
            _textHolder?.DOKill();
            _textHolder.DOLocalMoveX(_targetPosition, _duration);
            await UniTask.WaitForSeconds(1f);
            _textHolder.DOLocalMoveX(_initialPosition, _duration);
        }
    }
}