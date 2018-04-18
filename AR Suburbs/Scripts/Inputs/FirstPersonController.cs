#region Using Directives

using UnityEngine;

#endregion

namespace Assets.Scripts.Inputs
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Fields

        #region  Private Fields

        private Camera _camera;
        private CharacterController _characterController;
        private Vector2 _input;

        public SimpleTouchController leftController;
        public SimpleTouchController rightController;
        public float speedContinuousLook = 100f;
        public float speedProgressiveLook = 3000f;
        public bool continuousRightController = true;
        private Vector3 localEulRot;
        private Vector2 prevRightTouchPos;

        //[SerializeField]
        //private MouseLook _mouseLook;

        private Vector3 _moveDir = Vector3.zero;

        [SerializeField]
        private float _stickToGroundForce;

        [SerializeField]
        private float _walkSpeed;

        #endregion

        #endregion

        #region Methods

        #region Regular Methods

        private void Update()
        {
            //float speed;
            //GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            //var desiredMove = transform.forward * _input.y + transform.right * _input.x;
            var desiredMove = transform.forward * leftController.GetTouchPosition.y + transform.right * leftController.GetTouchPosition.x;
            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, _characterController.radius, Vector3.down, out hitInfo,
                               _characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
            _moveDir.x = desiredMove.x * _walkSpeed;
            _moveDir.z = desiredMove.z * _walkSpeed;
            if (_characterController.isGrounded)
                _moveDir.y = -_stickToGroundForce;
            _characterController.Move(_moveDir * Time.fixedDeltaTime);
            //_characterController.Move((transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * _walkSpeed) + (transform.right * leftController.GetTouchPosition.x * Time.deltaTime * _walkSpeed));
            //_mouseLook.UpdateCursorLock();
            //if (continuousRightController)
            //{
            //    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - rightController.GetTouchPosition.y * Time.deltaTime * speedContinuousLook,
            //        transform.localEulerAngles.y + rightController.GetTouchPosition.x * Time.deltaTime * speedContinuousLook,
            //        0f);
            //}
            var character = _characterController.transform;
            var camera = _characterController.GetComponentInChildren<Camera>().transform;
            var characterTargetRot = character.localRotation * Quaternion.Euler(0f, rightController.GetTouchPosition.x * 16f, 0f);
            var cameraTargetRot = camera.localRotation * Quaternion.Euler(-rightController.GetTouchPosition.y * 16f, 0f, 0f);
            character.localRotation = Quaternion.Slerp(character.localRotation, characterTargetRot, 5f * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, cameraTargetRot, 5f * Time.deltaTime);
            _characterController.transform.localRotation = character.localRotation;
            _characterController.GetComponentInChildren<Camera>().transform.localRotation = camera.localRotation;
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, -90f, 90f);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }
        //private void GetInput(out float speed)
        //{
        //    // Read input
        //    float horizontal = Input.GetAxis("Horizontal");
        //    float vertical = Input.GetAxis("Vertical");
        //    // set the desired speed to be walking or running
        //    speed = _walkSpeed;
        //    _input = new Vector2(horizontal, vertical);

        //    // normalize input if it exceeds 1 in combined length:
        //    if(_input.sqrMagnitude > 1)
        //        _input.Normalize();
        //}

        //private void RotateView()
        //{
        //    _mouseLook.LookRotation(transform, _camera.transform);
        //}

        // Use this for initialization
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            //_mouseLook.Init(transform, _camera.transform);
        }

        // Update is called once per frame
        //private void Update()
        //{
        //    //RotateView();
        //    // move
        //    //_rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
        //    //    (transform.right * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements));

        //}

        void Awake()
        {
            rightController.TouchEvent += RightController_TouchEvent;
            rightController.TouchStateEvent += RightController_TouchStateEvent;
        }

        public bool ContinuousRightController
        {
            set { continuousRightController = value; }
        }

        void RightController_TouchStateEvent(bool touchPresent)
        {
            if (!continuousRightController)
            {
                prevRightTouchPos = Vector2.zero;
            }
        }

        void RightController_TouchEvent(Vector2 value)
        {
            if (!continuousRightController)
            {
                Vector2 deltaValues = value - prevRightTouchPos;
                prevRightTouchPos = value;

                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - deltaValues.y * Time.deltaTime * speedProgressiveLook,
                    transform.localEulerAngles.y + deltaValues.x * Time.deltaTime * speedProgressiveLook,
                    0f);
            }
        }
        

        void OnDestroy()
        {
            rightController.TouchEvent -= RightController_TouchEvent;
            rightController.TouchStateEvent -= RightController_TouchStateEvent;
        }

        #endregion

        #endregion
    }
}