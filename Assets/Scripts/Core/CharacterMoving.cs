using UnityEngine;

[RequireComponent(typeof(Animator))]

public class CharacterMoving : MonoBehaviour
{
  public Sprite Portrait = null;
  [SerializeField] private float speedWalk = 1;
  [SerializeField] private float speedRun = 1;
  private float currentSpeed = 0;
  public Animator thisAnimator = null;
  [HideInInspector] public bool Block = true;
  /*[HideInInspector]*/ public bool KeyboardControl = true;
  [HideInInspector] public Direction AutoMoveDirection = Direction.None;
  private float TimeToHandle;
  private bool left = false;
  private bool right = false;
  private bool up = false;
  private bool down = false;

  //DV{
  private CircleCollider2D selfCollider;
  //DV}

    private void Awake ()
    {
        thisAnimator = GetComponent<Animator>();
        //DV{
        selfCollider = GetComponent<CircleCollider2D>();
        //DV}
    }

    
    void Update()
    {

        if (KeyboardControl && Block)
        {
          left = Input.GetKey(KeyCode.LeftArrow);
          right = Input.GetKey(KeyCode.RightArrow);
          up = Input.GetKey(KeyCode.UpArrow);
          down = Input.GetKey(KeyCode.DownArrow);
        }
        else
        {
          left = AutoMoveDirection == Direction.Left;
          right = AutoMoveDirection == Direction.Right;
          up = AutoMoveDirection == Direction.Up;
          down = AutoMoveDirection == Direction.Down;
        }

        MovingHandling();
    }


    private void MovingHandling()
  {
        float SpeedX = 0f, SpeedY = 0f;
        Vector3 deltaMove = new Vector3();

        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speedRun : speedWalk;

        if (up)
        {
            deltaMove += Vector3.up * Time.deltaTime * currentSpeed;
            SpeedY = 1;
        }

        if (down)
        {
            deltaMove -= Vector3.up * Time.deltaTime * currentSpeed;
            SpeedY = -1;
        }

        if (right)
        {
            deltaMove += Vector3.right * Time.deltaTime * currentSpeed;
            SpeedX = 1;
        }

        if (left)
        {
            deltaMove -= Vector3.right * Time.deltaTime * currentSpeed;
            SpeedX = -1;
        }

        //DV{
        if ((left || right) && (up || down)) //движение по диагонали
            deltaMove *= 0.7f;

        if (left || right || up || down)
        {
            thisAnimator.SetFloat("SpeedX", SpeedX);
            thisAnimator.SetFloat("SpeedY", SpeedY);

            //Если проход есть, проигрываем анимацию ходьбы и передвигаем ГГ, иначе - не двигаемся.
            Collider2D Obstacle = Physics2D.OverlapPoint((Vector2)transform.position + selfCollider.offset + new Vector2(SpeedX, SpeedY) * (selfCollider.radius + 2f));
            if (Obstacle == null || Obstacle.isTrigger)
            {
                transform.position += deltaMove;

                thisAnimator.SetBool("Running", true);
                thisAnimator.SetFloat("Speed", currentSpeed);
            }
            else
                thisAnimator.SetBool("Running", false);
        }
        else
            thisAnimator.SetBool("Running", false);
        //DV}
    }

    public void TurnTo(Direction Dir)
    {
        switch (Dir)
        {
            case Direction.Left:
                thisAnimator.SetFloat("SpeedX", -1);
                thisAnimator.SetFloat("SpeedY", 0);
                break;
            case Direction.Right:
                thisAnimator.SetFloat("SpeedX", 1);
                thisAnimator.SetFloat("SpeedY", 0);
                break;
            case Direction.Down:
                thisAnimator.SetFloat("SpeedX", 0);
                thisAnimator.SetFloat("SpeedY", -1);
                break;
            case Direction.Up:
                thisAnimator.SetFloat("SpeedX", 0);
                thisAnimator.SetFloat("SpeedY", 1);
                break;
            default:
                break;
        }
    }

    public void SetWind(bool sw)
    {
        thisAnimator.SetBool("Wind", sw);
    }
}
