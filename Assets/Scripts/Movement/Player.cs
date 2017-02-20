using UnityEngine;
using System.Collections;
using ControlWrapping;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(PlayerLight))]
public class Player : MonoBehaviour {

    
    public int life = 1;

    public Sprite deathSprite;

    private PlayerAnimation animationComponent;
    private PlayerMovement movementComponent;
    private PlayerShooting shootingComponent;
    private PlayerLight lightComponent;

    private Gamepad gamepad;

    int playerIndex;
    bool isPlayerAlive = true;

    public int PlayerIndex
    {
        get
        {
            return playerIndex;
        }
    }

    void Awake()
    {
        //Get Components
        animationComponent = GetComponent<PlayerAnimation>();
        movementComponent = GetComponent<PlayerMovement>();
        shootingComponent = GetComponent<PlayerShooting>();
        lightComponent = GetComponent<PlayerLight>();
    }

	// Use this for initialization
	void Start () {
        playerIndex = GameManager.AddPlayer(this);
        gamepad = ControllerManager.instance.RequestSpecificGamepad(playerIndex);
        
    }
	
	// Update is called once per frame
	void Update () {

        lightComponent.SetTransperency();

        if (!isPlayerAlive)
        {
            return;
        }
        //Move
        movementComponent.Move(gamepad.GetAxis(AxisCode.GamepadAxisLeftX),
            gamepad.GetAxis(AxisCode.GamepadAxisLeftY), 
            shootingComponent.IsShooting);

        //Aim
        shootingComponent.Aim(gamepad.GetAxis(AxisCode.GamepadAxisRightX), 
            gamepad.GetAxis(AxisCode.GamepadAxisRightY));

        //Shoot!
        if (gamepad.GetButtonDown(ActionKeyCode.GamepadRightTrigger)
            || gamepad.GetButtonDown(ActionKeyCode.GamepadLeftTrigger))
        {
            shootingComponent.Draw();
        }
        if (gamepad.GetButtonUp(ActionKeyCode.GamepadRightTrigger)
            || gamepad.GetButtonUp(ActionKeyCode.GamepadLeftTrigger))
        {
            shootingComponent.Shoot();
        }
    }

    /// <summary>
    /// Arrow hit player
    /// </summary>
    /// <param name="damage">an int</param>
    /// <returns>true if player died</returns>
    public bool OnShot(int damage)
    {
        life--;
        if (life <= 0)
        {
            Die();
            return true;
        }
        //play sound
        return false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Logger.Log("Player hit: " + other, this, LogLevel.Log);
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "GroundArrow")
        {
            shootingComponent.AddArrows(1);
            //ArrowRect.sizeDelta = new Vector2(ArrowRect.sizeDelta.x + 1, 1);

            //TODO: this should be done on the arrow
            otherObject.GetComponentInChildren<LightScript>().FadeAway(.25f);
            otherObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
            otherObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            Destroy(otherObject.gameObject, .25f);
        }
    }

    void Die()
    {
        isPlayerAlive = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }
}
