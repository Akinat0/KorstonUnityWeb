using Abu.Tools;
using UnityEngine;

public class ApartmentsController : MonoBehaviour
{
    [SerializeField] Transform root;
    [SerializeField] SpriteRenderer background;
    [SerializeField] float scrollSpeed = 2;
    [SerializeField] float characterMoveSpeed = 1.5f;
    [SerializeField] Transform character;
    [SerializeField] Transform characterMovementConstraint;
    [SerializeField] Camera mainCamera;
    [SerializeField] ApartmentsScroller scroller;

    float moveDirection = 0;
    public InteractableObject TargetInteractable { get; set; }
    public InteractableObject CurrentInteractable { get; set; }
    
    void Start()
    {
        float scale = ScreenScaler.FitVertical(background);
        root.localScale = Vector3.one * scale * 1.3f;
    }

    Vector2 prevScrollPos;
    
    
    void Update()
    {
        if (scroller.IsDragging)
        {
            
            Vector3 prev = mainCamera.ScreenToWorldPoint(scroller.PrevTouchPosition);
            Vector3 curr = mainCamera.ScreenToWorldPoint(scroller.CurrentTouchPosition);

            Vector3 delta = curr - prev;
            delta.y = 0;
            delta.z = 0;

            root.position += delta * scrollSpeed;

            scroller.PrevTouchPosition = scroller.CurrentTouchPosition;
        }
        
        // root.position += Vector3.right * moveDirection * scrollSpeed * Time.deltaTime;
        
        Rect spriteRectInWorld = GetSpriteRectInWorld(background);
        Rect cameraRect = GetCameraRect();

        if (cameraRect.xMin < spriteRectInWorld.xMin)
            root.position -= Vector3.right * (spriteRectInWorld.xMin - cameraRect.xMin);
        
        if (cameraRect.xMax > spriteRectInWorld.xMax)
            root.position -= Vector3.left * (cameraRect.xMax - spriteRectInWorld.xMax);


        if (!CurrentInteractable)
        {
            if (TargetInteractable)
            {
                if (Mathf.Approximately(character.position.x, TargetInteractable.InteractionPosition.x)
                    && Mathf.Approximately(character.position.y, TargetInteractable.InteractionPosition.y))
                {
                    CurrentInteractable = TargetInteractable;
                    TargetInteractable = null;
                    if (!CurrentInteractable.Interact())
                        CurrentInteractable = null;
                }
                else
                {
                    bool isRightXPos = Mathf.Approximately(TargetInteractable.InteractionPosition.x, character.position.x);
                
                    if (isRightXPos)
                    {
                        character.position = Vector3.MoveTowards(character.position, TargetInteractable.InteractionPosition, characterMoveSpeed * Time.deltaTime);
                    }
                    else
                    {
                        if (!IsCharOnBaseline())
                        {
                            character.position = Vector3.MoveTowards(character.position, characterMovementConstraint.position.SetX(character.position.x), characterMoveSpeed * Time.deltaTime);
                        }
                        else
                        {
                            character.position = Vector3.MoveTowards(character.position, characterMovementConstraint.position.SetX(TargetInteractable.InteractionPosition.x), characterMoveSpeed * Time.deltaTime);
                        }
                    }
                }
            }
            else
            {
                if (IsCharOnBaseline())
                {
                    Vector3 targetCharacterPos = new Vector3(0, characterMovementConstraint.position.y);
                    character.position = Vector3.MoveTowards(character.position, targetCharacterPos,
                        characterMoveSpeed * Time.deltaTime);
                }
                else
                {
                    character.position = Vector3.MoveTowards(character.position, character.position.SetY(characterMovementConstraint.position.y),
                        characterMoveSpeed * Time.deltaTime);
                }
                
            }
        }


    }


    bool IsCharOnBaseline()
    {
        return Mathf.Approximately(character.position.y, characterMovementConstraint.position.y);
    }

    void LateUpdate()
    {
        
    }

    public void MoveRight()
    {
        moveDirection = -1;
    }

    public void MoveLeft()
    {
        moveDirection = 1;
    }

    public void StopMoving()
    {
        moveDirection = 0;
    }
    
    
    public static Rect GetSpriteRectInWorld(SpriteRenderer spriteRenderer)
    {
        float width =  spriteRenderer.sprite.rect.width  / spriteRenderer.sprite.pixelsPerUnit;
        float height = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;

        width  *= spriteRenderer.transform.lossyScale.x;
        height *= spriteRenderer.transform.lossyScale.y;
        
        float xPos = spriteRenderer.transform.position.x - width / 2;
        float yPos = spriteRenderer.transform.position.y - height / 2;
            
        return new Rect(xPos, yPos, width, height);
    }
    
    public Rect GetCameraRect()
    {
        float width = mainCamera.aspect * 2f * mainCamera.orthographicSize;
        float height = 2f * mainCamera.orthographicSize;

        return new Rect(transform.position.x - width / 2, transform.position.y - height / 2, width, height);
    }

}
