using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Menu
{
    public class MenuItem : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [NaughtyAttributes.Button("Auto find image")]
        private void AutoFindImage()
        {
            _image = GetComponent<Image>();
        }

        public void UpdateItemIfSpriteNotNull(Sprite sprite)
        {
            if (_image != null && sprite != null)
            {
                _image.sprite = sprite;
            }
        }
    }
}