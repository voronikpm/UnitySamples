using System.Linq;
using Assets.Scripts.Building;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using Vuforia;

namespace Assets.Scripts
{
    [RequireComponent(typeof(StandaloneInputModule))]
    public class InputController : MonoBehaviour
    {
        public static Vector2 TouchPos;
        private void Update()
        {
            Input.simulateMouseWithTouches = false;
            bool isTouch = false;
            Vector2 touchPos = Vector2.zero;
            if (Application.isMobilePlatform)
            {
                if(Input.touchCount > 0)
                {
                    isTouch = Input.GetTouch(0).phase == TouchPhase.Began;
                    touchPos = Input.touches[0].position;
                }
            }
            else
            {
                isTouch = Input.GetMouseButtonDown(0);
                touchPos = Input.mousePosition;
            }
            if (isTouch)
            {
                if(!Camera.main)
                    return;
                var ray = Camera.main.ScreenPointToRay(touchPos);
                RaycastHit raycastHit;
                //MainSceneController.TouchPos = touchPos;
                TouchPos = touchPos;
                if (Physics.Raycast(ray, out raycastHit, 1 << LayerMask.NameToLayer("Clickable")))
                {
                    //if(MainSceneController.IsFirstPerson)
                    //{
                    var highlightedObject = raycastHit.transform.GetComponentInParent<HighlightableElement>() ?? raycastHit.transform.GetComponent<HighlightableElement>();
                        //if (highlightedObject && !highlightedObject.GetComponent<House>())
                    if(highlightedObject)
                    {
                        if(highlightedObject is House && highlightedObject.IsHighlighted)
                            (highlightedObject as House).LoadInterior();
                        else
                            highlightedObject.IsHighlighted = true;
                    }
                    //}
                    //else
                    //{
                    //    Debug.Log(raycastHit.transform.gameObject);
                    //    var house = raycastHit.transform.GetComponentInParent<House>() ?? raycastHit.transform.GetComponent<House>();
                    //    Debug.Log(house);
                    //    if (house)
                    //    {
                    //        if (!house.IsHighlighted && !house.IsSelected)
                    //            house.IsHighlighted = true;
                    //        else if (!house.IsSelected)
                    //            house.IsSelected = true;
                    //        else
                    //        {
                    //            var room = raycastHit.transform.GetComponentInParent<Room>() ?? raycastHit.transform.GetComponent<Room>();
                    //            if(room)
                    //            {
                    //                room.Load();
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
        }
    }
}