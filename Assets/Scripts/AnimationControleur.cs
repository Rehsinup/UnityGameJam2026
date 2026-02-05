using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationControleur : MonoBehaviour
{
    [System.Serializable]
    public class Animation
    {
        public string name;
        public bool loop = false;
        public bool stayOnLastFrame = false;
        public float frameRate = 30;
        public List<Sprite> frames = new List<Sprite>();
    }

    [Header("_animations (Animation 0 est l'Idle et Loop)")]
    [SerializeField] private List<Animation> _animations;

    [Header("System")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] UnityEvent<string> OnPlaySound;

    // Private variables 
    private int _currentAnimation = 0;
    private int _currentFrame = 0;
    private int _lastAnimationLoop = 0;

    private float _frameTimer = 0f;

    void Start()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        // Error checking for mandatory fields
        if (_spriteRenderer == null)
        {
            throw new System.Exception("spriteRenderer is not assigned in the inspector!");
        }
        if (_animations.Count == 0)
        {
            throw new System.Exception("No animations defined in the inspector!");
        }
        if (_animations[0].frames.Count == 0)
        {
            throw new System.Exception("The first animation (Idle) has no frames defined!");
        }
        _animations[0].loop = true;

        _spriteRenderer.sprite = _animations[0].frames[0];

    }

    void Update()
    {
        _frameTimer += Time.deltaTime;

        if (_frameTimer >= 1f / _animations[_currentAnimation].frameRate)
        {
            _frameTimer = 0f;

            _currentFrame++;

            if (_currentFrame >= _animations[_currentAnimation].frames.Count)
            {
                if (_animations[_currentAnimation].stayOnLastFrame)
                {
                    _currentFrame = _animations[_currentAnimation].frames.Count - 1;
                    return;
                }

                if (!_animations[_currentAnimation].loop)
                {
                    _currentAnimation = _lastAnimationLoop;
                }

                _currentFrame = 0;
            }


            _spriteRenderer.sprite = _animations[_currentAnimation].frames[_currentFrame];
        }
    }

    public void PlayAnimation(string animationName)
    {
        Animation animationToPlay = _animations.Find(anim => anim.name == animationName);
        OnPlaySound?.Invoke(animationName);
        if (animationToPlay != null)
        {
            if (_currentAnimation == _animations.IndexOf(animationToPlay))
                return;
            if (animationToPlay.loop)
                _lastAnimationLoop = _currentAnimation;
            _currentAnimation = _animations.IndexOf(animationToPlay);
            _frameTimer = 0f;
            _currentFrame = 0;
        }
        else
        {
            Debug.LogWarning($"Animation '{animationName}' not found!");
        }
    }
    public string GetCurrentAnimation()
    {
        return _animations[_currentAnimation].name;
    }
}
