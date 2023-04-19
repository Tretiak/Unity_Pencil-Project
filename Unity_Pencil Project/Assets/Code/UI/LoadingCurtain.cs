using System.Collections;
using UnityEngine;

namespace Code.UI
{
  public class LoadingCurtain : MonoBehaviour
  {
    [SerializeField]
    private float curtainAlpha = 0.03f;
    public CanvasGroup Curtain;

    private void Awake()
    {
      DontDestroyOnLoad(this);
    }

    public void Show()
    {
      gameObject.SetActive(true);
      Curtain.alpha = 1;
    }
    
    public void Hide() => StartCoroutine(DoFadeIn());
    
    private IEnumerator DoFadeIn()
    {
      while (Curtain.alpha > 0)
      {
        
        Curtain.alpha -= curtainAlpha;
        yield return new WaitForSeconds(curtainAlpha);
      }
      
      gameObject.SetActive(false);
    }
  }
}