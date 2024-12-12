using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorsPopUp : MonoBehaviour
{
    [SerializeField] private Scene _selectedScene;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _fadeImage;

    private const float ANIMATION_DURATION = 0.1f;
    private readonly Vector3 EXPANDED_SCALE_1 = Vector3.one;
    private readonly Vector3 EXPANDED_SCALE_2 = new(28f, 28f, 1f);
    private readonly Vector3 NORMAL_SCALE = Vector3.zero;

    private void Start()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void SceneTransition()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTextScale(_fadeImage.transform, EXPANDED_SCALE_2, () => { }, true));
    }


    public void ShowText()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTextScale(_text.transform, EXPANDED_SCALE_1));
    }

    public void HideText()
    {
        if (!gameObject.activeSelf)
            return;
        StartCoroutine(AnimateTextScale(_text.transform, NORMAL_SCALE, () =>
        {
            _text.gameObject.SetActive(false);
        }));

    }

    public void HideFade()
    {
        StartCoroutine(AnimateTextScale(_fadeImage.transform, NORMAL_SCALE, () =>
        {
            _fadeImage.gameObject.SetActive(false);
        }));
    }

    private void Initialize()
    {
        _text.transform.localScale = NORMAL_SCALE;
        _text.gameObject.SetActive(false);
        _fadeImage.transform.localScale = NORMAL_SCALE;
        _fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator AnimateTextScale(Transform objTransform, Vector3 targetScale, System.Action onComplete = null, bool isTransition = false)
    {
        objTransform.gameObject.SetActive(true);
        Vector3 startScale = objTransform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < ANIMATION_DURATION)
        {
            objTransform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / ANIMATION_DURATION);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objTransform.localScale = targetScale;
        if (isTransition)
            SceneLoader.Load(_selectedScene);
        onComplete?.Invoke();
    }
}
