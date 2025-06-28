using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject clearBackgroundImage;
    [SerializeField]
    private Image changeColorCooldownFilledImage;
    [SerializeField]
    private float changeColorCooldownTime = 1f;

    [SerializeField]
    private GameObject notificationGroup;
    [SerializeField]
    private TextMeshProUGUI notificationMessageText;
    [SerializeField]
    private float notificationShowDuration = 5f;
    [SerializeField]
    private Animator notificationAnimator;

    private int showTrigger;
    private int hideTrigger;

    private WaitForSeconds waitTime;

    public bool CanChangeMode { get; private set; } = true;

    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        showTrigger = Animator.StringToHash("Show");
        hideTrigger = Animator.StringToHash("Hide");

        waitTime = new WaitForSeconds(notificationShowDuration);
    }

    public void ShowClearBackgroundImage()
    {
        clearBackgroundImage.SetActive(true);
    }

    public void NotifyMessage(string message)
    {
        notificationMessageText.text = message;

        notificationGroup.SetActive(true);
        notificationAnimator.SetTrigger(showTrigger);

        StartCoroutine(DisableNotificationGroup());
    }

    private IEnumerator DisableNotificationGroup()
    {
        yield return waitTime;

        notificationAnimator.SetTrigger(hideTrigger);

        yield return waitTime;

        notificationGroup.SetActive(false);
    }

    public void OnChangedColor()
    {
        StartCoroutine(CooldownChangeColor());
    }

    private IEnumerator CooldownChangeColor()
    {
        CanChangeMode = false;

        for (float t = 0f; t < 1f; t += changeColorCooldownTime * Time.deltaTime)
        {
            changeColorCooldownFilledImage.fillAmount = t;

            yield return null;
        }

        changeColorCooldownFilledImage.fillAmount = 1f;
        CanChangeMode = true;
    }
}
