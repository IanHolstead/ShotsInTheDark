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

    private PlayerProfile profile;
    private PlayerControls controls;

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
        playerIndex = GM.AddPlayer(this);
        controls = new PlayerControls(GM.GetProfile(playerIndex).KeyBinding, playerIndex);
        
    }
	
	// Update is called once per frame
	void Update () {

        lightComponent.SetTransperency();

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
        //TODO: doesn't use damage being passed, just subtracts 1
        life--;
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
        Logger.Log("Player hit: " + other, this, LogLevel.Log);
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
