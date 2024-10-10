using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    Sequence leafSequence;
    SpriteRenderer leafRenderer;

    // Cache original values
    Vector3 originalPosition;
    Vector3 originalScale;
    Quaternion originalRotation;
    float originalAlpha;
    bool isUp = true, isShown = true, isFaded = false;

    private void Start()
    {
        // Get the SpriteRenderer component
        leafRenderer = GetComponent<SpriteRenderer>();
        if (leafRenderer == null)
        {
            Debug.LogError("No SpriteRenderer found on this GameObject.");
        }

        // Cache the original transform state and alpha
        originalPosition = transform.position;
        originalScale = transform.localScale;
        originalRotation = transform.rotation;
        originalAlpha = leafRenderer.color.a;

        // Initialize the sequence
        leafSequence = DOTween.Sequence();
    }

    public void Flash()
    {
        // Reset everything before starting the animation
        ResetToOriginalState();

        if (leafSequence == null)
        {
            Debug.LogWarning("leafSequence is not initialized.");
            return;
        }

        // Clear previous tweens in the sequence
        leafSequence.Kill();
        leafSequence = DOTween.Sequence();

        // Append tweens to the sequence
        leafSequence.Append(leafRenderer.DOFade(0, 0.2f)); // Fade out
        leafSequence.Append(leafRenderer.DOFade(1, 0.2f)); // Fade in
        leafSequence.Append(leafRenderer.DOFade(0, 0.2f)); // Fade out
        leafSequence.Append(leafRenderer.DOFade(1, 0.2f)); // Fade in
    }

    public void Shake()
    {
        // Reset everything before starting the animation
        ResetToOriginalState();

        if (leafSequence == null)
        {
            Debug.LogWarning("leafSequence is not initialized.");
            return;
        }

        // Clear previous tweens in the sequence
        leafSequence.Kill();
        leafSequence = DOTween.Sequence();

        // Append tweens to the sequence
        leafSequence.Append(transform.DOShakePosition(1.7f, new Vector3(1, 0, 0), 10, 0));
    }

    public void Tada()
    {
        // Reset everything before starting the animation
        ResetToOriginalState();

        if (leafSequence == null)
        {
            Debug.LogWarning("leafSequence is not initialized.");
            return;
        }

        // Clear previous tweens in the sequence
        leafSequence.Kill();
        leafSequence = DOTween.Sequence();

        // Append tweens to the sequence
        leafSequence.Append(transform.DOScale(0.9999f, 0.3f));
        leafSequence.Join(transform.DORotate(new Vector3(0, 0, 7), 0.3f));
        leafSequence.Append(transform.DOScale(1.3f, 0.3f));
        leafSequence.Join(transform.DORotate(new Vector3(0, 0, 0), 0.3f));
        leafSequence.Append(transform.DOShakeRotation(1f, new Vector3(0, 0, 4), 25, 10));
        leafSequence.Append(transform.DOScale(1.160423f, 0.3f));
    }
    
    public void Fly()
    {
        if (isUp)
        {
            // Reset everything before starting the animation
            ResetToOriginalState();

            if (leafSequence == null)
            {
                Debug.LogWarning("leafSequence is not initialized.");
                return;
            }

            // Clear previous tweens in the sequence
            leafSequence.Kill();
            leafSequence = DOTween.Sequence();

            leafSequence.Append(transform.DOMoveY(0.8f, 0.1f));
            leafSequence.Append(transform.DOMoveY(1.62f, 0.2f));
            leafSequence.Append(transform.DOMoveY(-12, 0.3f));
            isUp = false;
        } else
        {
            leafSequence = DOTween.Sequence();

            leafSequence.Append(transform.DOMoveY(1.62f, 0.1f));
            leafSequence.Append(transform.DOMoveY(0.06f, 0.1f));
            leafSequence.Append(transform.DOMoveY(1.01f, 0.1f));
            isUp = true;
        }
    }

    public void Drop()
    {
        if (isShown)
        {
            // Reset everything before starting the animation
            ResetToOriginalState();

            if (leafSequence == null)
            {
                Debug.LogWarning("leafSequence is not initialized.");
                return;
            }

            // Clear previous tweens in the sequence
            leafSequence.Kill();
            leafSequence = DOTween.Sequence();

            leafSequence.Append(transform.DOScale(0, 0.2f));
            leafSequence.Join(transform.DOMoveY(5.33f, 0.2f));
            leafSequence.Join(leafRenderer.DOFade(0, 0.2f));

            isShown = false;
        }
        else
        {
            leafSequence = DOTween.Sequence();
            leafSequence.Append(transform.DOScale(1.3f, 0.2f));
            leafSequence.Join(transform.DOMoveY(0.9f, 0.2f));
            leafSequence.Join(leafRenderer.DOFade(1, 0.2f));
            leafSequence.Append(transform.DOScale(originalScale, 0.2f));
            leafSequence.Join(transform.DOMoveY(1.01f, 0.2f));

            isShown = true;
        }
    }
    
    public void Fade()
    {
        if (!isFaded)
        {
            // Reset everything before starting the animation
            ResetToOriginalState();

            if (leafSequence == null)
            {
                Debug.LogWarning("leafSequence is not initialized.");
                return;
            }

            // Clear previous tweens in the sequence
            leafSequence.Kill();
            leafSequence = DOTween.Sequence();

            leafSequence.Append(leafRenderer.DOFade(0, 0.3f));

            isFaded = true;
        }
        else
        {
            leafSequence = DOTween.Sequence();
            leafSequence.Append(leafRenderer.DOFade(1, 0.3f));

            isFaded = false;
        }
    }


    public void ResetToOriginalState()
    {
        // Kill the current sequence
        if (leafSequence != null)
        {
            leafSequence.Kill();
        }

        // Reset transform values
        transform.position = originalPosition;
        transform.localScale = originalScale;
        transform.rotation = originalRotation;

        // Reset alpha of the SpriteRenderer
        Color color = leafRenderer.color;
        color.a = originalAlpha;
        leafRenderer.color = color;
        isUp = true;
        isShown = true;
        isFaded = false;
    }
}
