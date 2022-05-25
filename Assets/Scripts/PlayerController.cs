using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Sprint speed of the character in m/s")]
    public float SprintSpeed = 5.335f;

    [Tooltip("Crouch speed of the character in m/s")]
    public float CrouchSpeed = 1.0f;

    [Tooltip("Crouch speed of the character in m/s")]
    public float SlideSpeed = 1.0f;

    [Tooltip("Pull speed of the character in m/s")]
    public float PullSpeed = 1.0f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;

    [Space(10)]
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    // player
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDPush;
    private int _animIDPull;
    private int _animIDPressButton;
    private int _animIDCrouch;

    private PlayerInput _playerInput;

    private Animator _animator;
    private CharacterController _controller;
    private Inputs _input;
    private GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private bool _hasAnimator;

    public bool _inElevator;

    private bool _crouched;
    private bool _crouch;

    public bool Pulling;

    public bool _inBelt;

    private Rigidbody _rigidbody;
    private PlatformController _platformController;
    private Switch _switchController;
    private LeverController _leverController;
    private ButtonController _buttonController;
    private FieldOfView _fieldOfView;

    private float _controllerHeight;
    private Vector3 _controllerCenter;
    [SerializeField]
    private Transform _crouchPoint;

    [SerializeField]
    private float _beltVelocity;

    public float _worldLimitZ = -19.5f;

    public bool boatTravel;

    public float _fallingDistance;
    private float _lastGroundedPositionY;

    [SerializeField]
    private float _maxFallingDistance;

    private bool IsCurrentDeviceMouse
    {
        get
        {

            return _playerInput.currentControlScheme == "KeyboardMouse";

        }
    }


    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
        _hasAnimator = TryGetComponent(out _animator);
        _controller = GetComponent<CharacterController>();
        _rigidbody = GetComponent<Rigidbody>();

        _controllerCenter = _controller.center;
        _controllerHeight = _controller.height;

        _input = GetComponent<Inputs>();

        _playerInput = GetComponent<PlayerInput>();

        _fieldOfView = GetComponent<FieldOfView>();


        AssignAnimationIDs();

        // reset our timeouts on start
        _jumpTimeoutDelta = JumpTimeout;
        _fallTimeoutDelta = FallTimeout;
    }

    private void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);

        JumpAndGravity();
        GroundedCheck();
        if (!boatTravel)
        {
            Move();
        }
        StillCrouched();
        
    }

    private void FixedUpdate()
    {
        MoveBelt();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDPush = Animator.StringToHash("Pushing");
        _animIDPull = Animator.StringToHash("Pulling");
        _animIDPressButton = Animator.StringToHash("PressButton");
        _animIDCrouch = Animator.StringToHash("Crouch");
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);

        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDGrounded, Grounded);
        }
    }

    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private void Move()
    {
        // set target speed based on move speed, sprint speed and if sprint is pressed
        float targetSpeed = _input.sprint && !_crouched  ? SprintSpeed : MoveSpeed;
        if (_crouched)
        {
            targetSpeed = CrouchSpeed;
        }
        else if (Pulling) {
            targetSpeed = PullSpeed;
        }

        // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

        // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is no input, set the target speed to 0
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // a reference to the players current horizontal velocity
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
        // if there is a move input rotate player when the player is moving
        if (_input.move != Vector2.zero && !Pulling)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }
        else {
            _animator.SetBool(_animIDPush, false);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

       
        // move the player
        if (_controller.enabled)
        {
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                                new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }
        else {
            _rigidbody.AddForce(new Vector3(0, 0, targetDirection.normalized.z) * (SlideSpeed * Time.deltaTime), ForceMode.Force);
        }

        

        if (transform.position.z < _worldLimitZ) {
            transform.position = new Vector3(transform.position.x, transform.position.y, _worldLimitZ);
        }

        // update animator if using character
        if (_hasAnimator)
        {
            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);
            }

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (_input.jump && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, true);
                }
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
            _lastGroundedPositionY = transform.position.y;
        }
        else
        {

            _fallingDistance = _lastGroundedPositionY - transform.position.y;
            Debug.Log("Falling distance: " + _fallingDistance);
            if (_fallingDistance > _maxFallingDistance) {
                GetComponent<RespawnSystem>().KillPlayer("Free Falling");
            }

            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }
            }

            // if we are not grounded, do not jump
            _input.jump = false;
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void MoveBelt() {
        if (_inBelt)
        {
            transform.position = new Vector3(transform.position.x + (_beltVelocity * Time.deltaTime), transform.position.y, transform.position.z);
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

        if (Grounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;

        // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
        Gizmos.DrawSphere(
            new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
            GroundedRadius);
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                var index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
        }
    }

    private void OnAction() {
        if (_platformController != null) {
            if (_platformController.ButtonUp != null && _platformController.ButtonDown != null)
            {
                Debug.Log("FieldOfView: " + (_fieldOfView.objectInFOV == _platformController.ButtonUp));
                if (_fieldOfView.objectInFOV == _platformController.ButtonUp)
                {
                    _platformController.GoUp(this);
                }
                if (_fieldOfView.objectInFOV == _platformController.ButtonDown)
                {
                    _platformController.GoDown(this);
                }
            }
            if (_platformController.Lever.parent != null)
            {
                if (_fieldOfView.objectInFOV == _platformController.Lever.parent)
                {
                    _platformController.action(this);
                }
            }
        }
        if(_switchController != null){
            _switchController.action();
        }
        if (_leverController != null) {
            _leverController.action(this);
        }
        if (_buttonController != null) {
            _buttonController.action(this);
        }
    }

    private void OnCrouch()
    {
        _crouch = true;
        _crouched = true;
        _controller.height = 1.3f;
        _controller.center = new Vector3(_controller.center.x, 0.66f, _controller.center.z);
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDCrouch, true);
        }
    }

    private void OnCrouchEnd()
    {
        _crouch = false;
        RaycastHit hit;
        if (!Physics.Raycast(_crouchPoint.position, _crouchPoint.TransformDirection(Vector3.up), out hit, 2)) {
            _controller.height = _controllerHeight;
            _controller.center = _controllerCenter;
            _crouched = false;
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDCrouch, false);
            }
        }
    }

    private void StillCrouched() {
        if (!_crouch && _controller.height != _controllerHeight)
        {
            RaycastHit hit;
            if (!Physics.Raycast(_crouchPoint.position, _crouchPoint.TransformDirection(Vector3.up), out hit, 2))
            {
                _crouched = false;
                _controller.height = _controllerHeight;
                _controller.center = _controllerCenter;
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDCrouch, false);
                }
            }
        }
    }

    public void actionStart()
    {
        if (_platformController || _leverController != null || _buttonController != null) {
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDPressButton, true);
            }
        }
    }

    public void PressButtonEnd()
    {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDPressButton, false);
        }
    }

    public void actionEnd() {
        if (_hasAnimator)
        {
            _animator.SetBool(_animIDPressButton, false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platform") {
            _platformController = other.GetComponent<PlatformController>();
            transform.parent = other.GetComponent<Collider>().transform;
        }

        if (other.tag == "Switch") {
            _switchController = other.GetComponent<Switch>();
        }

        if (other.tag == "Lever") {
            _leverController = other.GetComponent<LeverController>();
        }

        if (other.tag == "Button") {
            _buttonController = other.GetComponent<ButtonController>();
        }

        if (other.tag == "Belt") {
            _inBelt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform")
        {
            _platformController = null;
            transform.parent = null;
            actionEnd();
        }

        if (other.tag == "Lever")
        {
            _leverController = null;
            actionEnd();
        }

        if (other.tag == "Button")
        {
            _buttonController = null;
            actionEnd();
        }

        if (other.tag == "Belt")
        {
            _inBelt = false;
        }

    }

}