using System;
using System.Collections;
using Luna.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Luna.Game.Player
{
    public class PlayerController : MonoBehaviour, InputActions.IGameplayActions
    {
        public float moveSpeed;
        public float rotateSpeed;
        public float burstSpeed;
        public GameObject projectile;

        private InputActions inputActions;
        private bool m_Charging;
        private Vector2 m_Rotation;

        public void Awake()
        {
            inputActions = new InputActions();
            inputActions.Gameplay.AddCallbacks(this);
        }

        public void OnEnable()
        {
            inputActions.Enable();
        }

        public void Start()
        {

        }

        public void OnDisable()
        {
            inputActions.Disable();
        }

        public void OnGUI()
        {
            if (m_Charging)
                GUI.Label(new Rect(100, 100, 200, 100), "Charging...");
        }

        public void Update()
        {
            var look = inputActions.Gameplay.Look.ReadValue<Vector2>();
            var move = inputActions.Gameplay.Move.ReadValue<Vector2>();

            // Update orientation first, then move. Otherwise move orientation will lag
            // behind by one frame.
            Look(look);
            // Move(move);
        }

        private void Move(Vector2 direction)
        {
            if (direction.sqrMagnitude < 0.01)
                return;
            var scaledMoveSpeed = moveSpeed * Time.deltaTime;
            // For simplicity's sake, we just keep movement in a single plane here. Rotate
            // direction according to world Y rotation of player.
            var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
            transform.position += move * scaledMoveSpeed;
        }

        private void Look(Vector2 rotate)
        {
            if (rotate.sqrMagnitude < 0.01)
                return;
            var scaledRotateSpeed = rotateSpeed * Time.deltaTime;
            m_Rotation.y += rotate.x * scaledRotateSpeed;
            m_Rotation.x = Mathf.Clamp(m_Rotation.x - rotate.y * scaledRotateSpeed, -89, 89);
            transform.localEulerAngles = m_Rotation;
        }

        private IEnumerator BurstFire(int burstAmount)
        {
            for (var i = 0; i < burstAmount; ++i)
            {
                Fire();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void Fire()
        {
            var transform = this.transform;

            var newProjectile = Instantiate(projectile);
            newProjectile.transform.position = transform.position + transform.forward * 0.6f;
            newProjectile.transform.rotation = transform.rotation;
            const int size = 1;
            newProjectile.transform.localScale *= size;
            newProjectile.GetComponent<Rigidbody>().mass = Mathf.Pow(size, 3);
            newProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 20f, ForceMode.Impulse);
            newProjectile.GetComponent<MeshRenderer>().material.color =
                new Color(Random.value, Random.value, Random.value, 1.0f);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (context.interaction is SlowTapInteraction)
                    m_Charging = true;
            }

            if (context.performed)
            {
                if (context.interaction is SlowTapInteraction)
                {
                    StartCoroutine(BurstFire((int)(context.duration * burstSpeed)));
                }
                else
                {
                    Fire();
                }
            }

            if (context.canceled)
            {
                m_Charging = false;
            }
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPunch(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnProne(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPickup(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnOpenInventory(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot1(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot2(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot3(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot4(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot5(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot6(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot7(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot8(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot9(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnSlot10(InputAction.CallbackContext context)
        {
            throw new NotImplementedException();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(@"Moveing...");
            var move = context.ReadValue<Vector2>();
            Move(move);
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

    }

}
