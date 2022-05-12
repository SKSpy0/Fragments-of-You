using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour {

   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color backgroundActiveColor ;
   [SerializeField] Color handleActiveColor ;

   Image backgroundImage, handleImage ;
   Color backgroundDefaultColor, handleDefaultColor ;

   Toggle toggle ;
   bool savedToggle;
   

   Vector2 handlePosition;

   GameManager gameManager;

   void Awake ( ) {
      toggle = GetComponent <Toggle> ( );

      handlePosition = uiHandleRectTransform.anchoredPosition;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image> ( );
      handleImage = uiHandleRectTransform.GetComponent <Image> ( );

      backgroundDefaultColor = backgroundImage.color;
      handleDefaultColor = handleImage.color;

      toggle.onValueChanged.AddListener(OnSwitch);

      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

      toggle.isOn = gameManager.LoadingButtonSettings(this.gameObject.name);
      savedToggle = toggle.isOn;

      if (toggle.isOn)
         OnSwitch (true) ;
   }

   public void toggleSFXP()
   {
      if (savedToggle == true)
      {
         OnSwitch (false);
         PlayerPrefs.SetFloat(this.gameObject.name, 0);
      }
        
      if (savedToggle == false)
      {
         OnSwitch (true);
         PlayerPrefs.SetFloat(this.gameObject.name, 1);
      }
   }

   public void toggleGODMODE()
   {
      if (toggle.isOn == true)
      {
         OnSwitch (true);
         GameManager.GodMode = true;
         Debug.Log("God Mode is now turned on.");
         PlayerPrefs.SetFloat(this.gameObject.name, 1);
      }
      else{
         OnSwitch (false);
         GameManager.GodMode = false;
         Debug.Log("God Mode is now turned off.");
         PlayerPrefs.SetFloat(this.gameObject.name, 0);
      }
   }
   
   void OnSwitch (bool on) {
      // uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
      uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack) ;

      // backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
      backgroundImage.DOColor (on ? backgroundActiveColor : backgroundDefaultColor, .6f) ;

      // handleImage.color = on ? handleActiveColor : handleDefaultColor ; // no anim
      handleImage.DOColor (on ? handleActiveColor : handleDefaultColor, .4f) ;
   }

   void OnDestroy () {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }
}
