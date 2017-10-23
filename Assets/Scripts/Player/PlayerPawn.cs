using UnityEngine;
using System.Collections;
using ControlWrapping;

[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(PlayerLight))]
public class PlayerPawn : MonoBehaviour {

    
    public int life = 1;

    public Sprite deathSprite;

    private PlayerAnimation animationComponent;
    private PlayerMovement movementComponent;
    private PlayerShooting shootingComponent;
    private PlayerLight lightComponent;

    private Player playerParent;

    private PlayerInput controls;
    
    bool isPlayerAlive = true;

    public int PlayerIndex
    {
        get
        {
            return playerParent.PlayerID;
        }
    }

    public bool IsPlayerAlive
    {
        get
        {
            return isPlayerAlive;
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
    
	void Start () {
        controls = new PlayerInput(playerParent.Profile.KeyBinding, PlayerIndex);
    }
	
	void Update () {

        lightComponent.UpdateTransperency();

        if (!isPlayerAlive)
        {
            return;
        }
        //Move
        movementComponent.Move(controls.GetAxis(AxisControl.moveX), 
            controls.GetAxis(AxisControl.moveY), shootingComponent.IsShooting);

        //Aim
        shootingComponent.Aim(controls.GetAxis(AxisControl.aimX),
            controls.GetAxis(AxisControl.aimY), 
            controls.GetAxis(AxisControl.moveX),
            controls.GetAxis(AxisControl.moveY));

        //Shoot!
        if (controls.GetKeyDown(ActionControl.shoot))
        {
            shootingComponent.Draw();
        }
        if (controls.GetKeyUp(ActionControl.shoot))
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
        life = life - damage;
        if (life <= 0)
        {
            Die();
            return true;
        }
        //TODO: play sound
        return false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Logger.Log("Player hit: " + other, this, LogLevel.Verbose);
    }

    void Die()
    {
        isPlayerAlive = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().sprite = deathSprite;
    }

    public void SetPlayerParent(Player parent)
    {
        playerParent = parent;
    }

    public void SetInitialDirection(Quaternion direction)
    {
        Vector3 vectorDir = direction * Vector3.up;
        animationComponent.MovementAnimation(-vectorDir.x, vectorDir.y);
    }
}
