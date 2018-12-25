using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using System;

public class PlayerControllerAdvanced : MonoBehaviour {

    public CharacterController2D.CharacterCollisionState2D flags;

    //Player Control Parameters
    public float walkSpeed = 6.0f;
    public float jumpSpeed = 20.0f;
    public float gravity = 20.0f;
    public float doubleJumpspeed = 10.0f;
    public float wallXJumpAmount = 0.75f;
    public float wallYJumpAmount = 1.0f;
    public float wallRunAmount = 2f;
    public float slopeSlideSpeed = 4f;
    public float glideAmount = 2f;
    public float glideTimer = 2f;
    public float creepSpeed = 3.0f;
    public float powerJumprSpeed = 10.0f;
    public float stompSpeed = 4.0f;


    // Player Habilities Toggles
    public bool canDoubleJump = true;
    public bool canWallJump = true;
    public bool canWallRun = true;
    public bool canJumpAfterWallJump = true;
    public bool canRunAfterWallJump = true;
    public bool canGlide = true;
    public bool canPowerJump = true;
    public bool canStomp = true;

    // Layer Mask
    public LayerMask layerMask;

    // Player State Habilities
    public bool doubleJumped;
    public bool isGrounded;
    public bool isJumping;
    public bool isFacingRight;
    public bool wallJumped;
    public bool isWallRuning;
    public bool isSlopeSlading;
    public bool isGliding;
    public bool isDucking;
    public bool isCreeping;
    public bool isPowerJumping;
    public bool isStomping;

    // Private Variables
    private Vector2 _moveDirection = Vector3.zero;
    private CharacterController2D _characterController;
    private bool _lastJumpedwasLeft;
    private float _slopeAngle;
    private Vector3 _slopeGradient = Vector3.zero;
    private bool _startGlide;
    private float _currentGlideTimer;
    private BoxCollider2D _boxCollider;
    private Vector2 _originalBoxColliderSize;
    private Vector3 _fronTopCorner;
    private Vector3 _backTopCorner;
   
    // Other Variables
    SpriteRenderer sp;
    private Animator anim;
    public GameObject btnFly;

    // MobileButton
    bool leftpressed;
    bool rightpressed;
    bool jumppressed;
    bool flypressed;

    bool unaSolavez;
   

	void Start () {

        unaSolavez = true;
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        _characterController = GetComponent<CharacterController2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
         canDoubleJump = true;
        _currentGlideTimer = glideTimer;

        btnFly.SetActive(false);

         //This variable save original size the our boxcollider;
        _originalBoxColliderSize = _boxCollider.size;
	
    }
	
	
	void Update () {

        //Cuando uso los controles en Pc
        if(wallJumped == false){

             //Give a number between -1 to 1
            _moveDirection.x = Input.GetAxis("Horizontal");
            _moveDirection.x *= walkSpeed;

           
        }
        //Cuando uso los controles en Mobile
        Move();

        // Deslizamiento 
        RaycastHit2D hit = Physics2D.Raycast(transform.position,-Vector3.up, 2f , layerMask);

        if(hit){

            _slopeAngle = Vector2.Angle(hit.normal,Vector2.up);
            _slopeGradient = hit.normal;


            if(_slopeAngle > _characterController.slopeLimit){
               
                isSlopeSlading = true;
            
            } else {

                isSlopeSlading = false;
            }
        }

        // The player is in the ground
        if(isGrounded){
            
            _moveDirection.y = 0;
            isJumping = false;
            doubleJumped = false;
            isStomping = false;
            _currentGlideTimer = glideTimer;

            if(btnFly.activeSelf){
                btnFly.SetActive(false);
            }

            DireccionEnPiso();

            CuestaAbajo();

            Saltar();

        //The player is in the air ... 
        } else {

            //Cuando el personaje cae
            Falling();

            DoubleJump();

            DireccionEnElAire();

        }

        Fly();

        // Character - Move  
        _characterController.move(_moveDirection * Time.deltaTime);

        //Marca el estado de las collisiones
        flags = _characterController.collisionState;

        isGrounded = flags.below;

        if(flags.above){

             // Se le aplica la gravedad a Toba
            _moveDirection.y -= gravity * Time.deltaTime;
        
        }




        /*  _fronTopCorner = new Vector3(transform.position.x + _boxCollider.size.x /2,transform.position.y + +_boxCollider.size.y / 2,0);
          _backTopCorner = new Vector3(transform.position.x + _boxCollider.size.x / 2, transform.position.y + _boxCollider.size.y / 2,0);

          RaycastHit2D hitFrontCeiling = Physics2D.Raycast(_fronTopCorner, Vector2.up,2f,layerMask);
          RaycastHit2D hitBackCeiling = Physics2D.Raycast(_backTopCorner, Vector2.up, 2f, layerMask);



          // El personaje se agacha y se arrastra
          if(Input.GetAxis("Vertical") < 0 && _moveDirection.x == 0){

              if(!isDucking && !isCreeping){

                   // Aca puede haber un error
                  _boxCollider.size = new Vector2(_boxCollider.size.x + _originalBoxColliderSize.x/2, _boxCollider.size.y + _originalBoxColliderSize.y / 2);
                  transform.position = new Vector3(transform.position.x, transform.position.y - (_originalBoxColliderSize.y / 4), 0);
                  _characterController.recalculateDistanceBetweenRays();

              }

              isDucking = true;
              isCreeping = false;

          // El personaje se agacha pero y se mueve
          } else if(Input.GetAxis("Vertical") < 0 && (_moveDirection.x < 0 || _moveDirection.x > 0)){

              if (!isDucking && !isCreeping)
              {

                  _boxCollider.size = new Vector2(_boxCollider.size.x, _originalBoxColliderSize.y / 2);
                 transform.position = new Vector3(transform.position.x, (transform.position.y - (_originalBoxColliderSize.y / 4)), 0);
                  _characterController.recalculateDistanceBetweenRays();

              }

              isDucking = false;
              isCreeping = true;

              // El personaje se para
          } else {

              if ((!hitFrontCeiling.collider && !hitBackCeiling.collider) && isDucking || isCreeping)
              {
                  _boxCollider.size = new Vector2(_boxCollider.size.x, _originalBoxColliderSize.y);
                  transform.position = new Vector3(transform.position.x,transform.position.y +  (_originalBoxColliderSize.y / 4),0);
                  _characterController.recalculateDistanceBetweenRays();
                  isDucking = false;
                  isCreeping = false; 
              }

          }*/

        WallRunWalk();


	}

    private void Move()
    {
        if(rightpressed){

            if (wallJumped == false)
            {

                    _moveDirection.x += 0.9f;
                    _moveDirection.x *= walkSpeed;

            }

          
        } else if(leftpressed) {
           
            if (wallJumped == false)
            {

                    _moveDirection.x += -0.9f;
                    _moveDirection.x *= walkSpeed;

            }

        }

        _characterController.move(_moveDirection * Time.deltaTime); 

    }

    private void WallRunWalk()
    {
        if (flags.left || flags.right)
        {
            // Wall / Run
            if (canWallRun)
            {

                if (Input.GetAxis("Vertical") > 0 && isWallRuning == true)
                {
                    
                    _moveDirection.y = jumpSpeed / wallRunAmount;
                    StartCoroutine(WallRunWaiter());
                }

            }
            // Wall / jump
            if(canWallJump){

                if ((Input.GetButtonDown("Jump") || jumppressed) && wallJumped == false && isGrounded == false)
                { 
                    if(_moveDirection.x < 0){

                        _moveDirection.x = jumpSpeed * wallXJumpAmount;
                        _moveDirection.y = jumpSpeed * wallYJumpAmount;
                        transform.eulerAngles = new Vector3(0,0,0);
                        _lastJumpedwasLeft = false;
                    
                    } else if(_moveDirection.x > 0) {


                        _moveDirection.x = -jumpSpeed * wallXJumpAmount;
                        _moveDirection.y = jumpSpeed * wallYJumpAmount;
                        transform.eulerAngles = new Vector3(0,180, 0);
                        _lastJumpedwasLeft = true;
                    
                    }

                    StartCoroutine(WallJumpWaiter());

                    if (canJumpAfterWallJump)
                    {
 
                        doubleJumped = false;

                    }
                
                }
            
            }
        
        } else {
           
            if(canRunAfterWallJump){
                
                StopCoroutine(WallRunWaiter());
                isWallRuning = true;
            
            }
        }
    
    }

    private void DireccionEnElAire()
    {

        //Cuando se mueve a la izquierda
        if (_moveDirection.x < 0)
        {

            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;


        }
        else if (_moveDirection.x > 0)
        {


            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;


        }
    }

    private void DoubleJump()
    {

        if (Input.GetButtonUp("Jump") || jumppressed)
        {
            //Si esta subiendo en el aire.
            if (_moveDirection.y > 0)
            {
                
                anim.SetInteger("state", 2);

                if(jumppressed){
                
                    _moveDirection.y = _moveDirection.y * 0.8f;
                    // Lo pongo en falso para poder hacer el doble salto.
                    jumppressed = false;

                
                } else {

                    _moveDirection.y = _moveDirection.y * .5f;

                }


            }

        }


        // Double Jump ...
        if (Input.GetButtonDown("Jump") || jumppressed)
        {
            if (canDoubleJump)
            {

                if (!doubleJumped)
                {


                    _moveDirection.y = doubleJumpspeed;
                    doubleJumped = true;

                }
            }

        }
    }

    private void Falling()
    {
       //Si el personaje esta bajando
        if (_moveDirection.y <= 0)
            {
           
            // Cuando cae el personaje
            anim.SetInteger("state", 3);
       } 
    }

    private void Fly()
    {
        //Gravity Calculations
        if (canGlide && (Input.GetAxis("Vertical") > 0.5f || flypressed) && _characterController.velocity.y < 0.2f)
        {


            if (_currentGlideTimer > 0)
            {

                isGliding = true;

                if (_startGlide)
                {

                    _moveDirection.y = 0;
                    _startGlide = false;

                }

                _moveDirection.y -= glideAmount * Time.deltaTime;
                _currentGlideTimer -= Time.deltaTime;

            }
            else
            {

                btnFly.SetActive(false);
                isGliding = false;
                flypressed = false;
                _moveDirection.y -= gravity * Time.deltaTime;

            }

        }
        else if (canStomp && isDucking && !isPowerJumping)
        {

            _moveDirection.y -= gravity * Time.deltaTime + stompSpeed;


        }
        else
        {

            isGliding = false;
            _startGlide = true;
            _moveDirection.y -= gravity * Time.deltaTime;
        }

    }

    private void CuestaAbajo()
    {
         // Cuando esta cuesta abajo el personaje
            if (isSlopeSlading)
        {
            _moveDirection = new Vector3(_slopeGradient.x * slopeSlideSpeed, -_slopeGradient.y * slopeSlideSpeed, 0);

        }

    }

    //Saltando desde el suelo
    private void Saltar(){
        // Jumping ...
        if (Input.GetButtonDown("Jump") || jumppressed)
        {

           
            // Entra esta condicion si esta agachado y tiene la posibilidad de agachar.
            if (canPowerJump && isDucking)
            {

                _moveDirection.y = jumpSpeed + powerJumprSpeed;
                StartCoroutine("PowerJumpWaiter");

            }
            else
            {

                //Activo el boton para planear
                btnFly.SetActive(true);
                anim.SetInteger("state", 2);
                _moveDirection.y = jumpSpeed;
                isJumping = true;
            }

            isWallRuning = true;

        }

    }



     private void DireccionEnPiso(){
       
        //Cuando se mueve a la izquierda
        if (_moveDirection.x < 0)
        {

            anim.SetInteger("state", 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;


        } //Cuando se mueve a la derecha
        else if (_moveDirection.x > 0)
        {

            anim.SetInteger("state", 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;


        }//Cuando se queda quieto
        else if (_moveDirection.x == 0)
        {

            anim.SetInteger("state", 0);

        }
    }


    IEnumerator WallJumpWaiter(){
        wallJumped = true;
        yield return new WaitForSeconds(0.5f);
        wallJumped = false;
    }


    IEnumerator WallRunWaiter()
    {
        isWallRuning = true;
        yield return new WaitForSeconds(0.5f);
        isWallRuning = false;
    }


    //
    IEnumerator PowerJumpWaiter()
    {
        isPowerJumping = true;
        yield return new WaitForSeconds(1f);
        isPowerJumping = false;
    }


   
    //Controlo las interacciones con los personajes
    void OnTriggerEnter2D(Collider2D other)
    {

         if (other.gameObject.tag == "Enemigos" || other.gameObject.tag == "Orquito" || other.gameObject.tag == "Spike" || 
             other.gameObject.tag == "OrquitoTutorial")
        {
            if(unaSolavez){
                unaSolavez = false;
                AudioCtrl.instance.PlayerDied(gameObject.transform);
                SistemaDejuego.instance.PlayerDiedAnimaton(gameObject);

            }

        }

       
    }

    /// <summary>
    ///      Mobile Part
    /// </summary>
    public void MoveRight()
    {
        rightpressed = true;
        leftpressed = false;
    }

    public void MoveLeft()
    {
        leftpressed = true;
        rightpressed = false;
    }
    public void Stop()
    {
        rightpressed = false;
        leftpressed = false;
        jumppressed = false;
        flypressed = false;
    }

    public void MobileJump()
    {
        jumppressed = true;
    }

    public void FlyMobile(){
        flypressed = true;
    }

    public void StopJump()
    {
        jumppressed = false;
        flypressed = false;
    }

}
