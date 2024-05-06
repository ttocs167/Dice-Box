using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CollapseMenu : MonoBehaviour
    {
        private bool _isCollapsed = false;
        private RectTransform _rectTransform;
        public Button expandCollapseButton;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            expandCollapseButton.onClick.AddListener(OnCollapseMenu);
        }
        
        private void OnCollapseMenu()
        {
            if (_isCollapsed)
            {
                ExpandMenuAnim();
            }
            else
            {
                CollapseMenuAnim();
            }
        }
        
        private void CollapseMenuAnim()
        {
            _rectTransform.DOLocalMoveX(-1040, 1).OnComplete(() => _isCollapsed = true);
        }
        
        private void ExpandMenuAnim()
        {
            _rectTransform.DOLocalMoveX(-550, 1).OnComplete(() => _isCollapsed = false);
        }
    }
}
