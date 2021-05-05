 using UnityEngine;
 
 public class ScreenBounds : MonoBehaviour
 {
     GameObject top;
     GameObject bottom;
     GameObject left;
     GameObject right;
 
 
     void Awake()
     {
         top = new GameObject("ScreenCollider_Top") {tag = "Top"};
         bottom = new GameObject("ScreenCollider_Bottom") {tag = "Bottom"};
         left = new GameObject("ScreenCollider_Left") {tag = "Left"};
         right = new GameObject("ScreenCollider_Right") {tag = "Right"};
     }
 
     void Start()
     {
         CreateScreenColliders(true);
         CreateScreenColliders(false);
     }
 
     void CreateScreenColliders(bool isTrigger)
     {
         Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
         Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
 
         // Create top collider
         BoxCollider2D collider = top.AddComponent<BoxCollider2D>();
         collider.isTrigger = isTrigger;
         collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
         collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
         top.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, topRightScreenPoint.y, 0f);
 
         // Create bottom collider
         collider = bottom.AddComponent<BoxCollider2D>();
         collider.isTrigger = isTrigger;
         collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x), 0.1f, 0f);
         collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
         bottom.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f, bottomLeftScreenPoint.y - collider.size.y, 0f);
 
         // Create left collider
         collider = left.AddComponent<BoxCollider2D>();         
         collider.isTrigger = isTrigger;
         collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
         collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
         left.transform.position = new Vector3(((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2f) - collider.size.x, bottomLeftScreenPoint.y, 0f);
 
         // Create right collider
         collider = right.AddComponent<BoxCollider2D>();
         collider.isTrigger = isTrigger;
         collider.size = new Vector3(0.1f, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y), 0f);
         collider.offset = new Vector2(collider.size.x / 2f, collider.size.y / 2f);
         right.transform.position = new Vector3(topRightScreenPoint.x, bottomLeftScreenPoint.y, 0f);
     }
 }