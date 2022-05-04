using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float jumpSpeed = 15f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float maxJumpHeight = 10f;

    private Rigidbody _rigidbody;

    private Quaternion targetRotation = Quaternion.identity;
    private float _startJumpPositionY;

    private bool _isJumping;
    private JumpPhase _jumpPhase;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rigidbody.position += Vector3.right * Time.deltaTime * movementSpeed;
        TryJump();
    }

    private void TryJump()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _startJumpPositionY = _rigidbody.position.y;
            _isJumping = true;
            _jumpPhase = JumpPhase.Rise;
        }

        _rigidbody.useGravity = !_isJumping;

        if (!_isJumping)
            return;

        if(_jumpPhase is JumpPhase.Rise)
        {
        //Debug.Log((_startJumpPositionY + maxJumpHeight) - _rigidbody.position.y);
            if ((_startJumpPositionY + maxJumpHeight) - _rigidbody.position.y > 0.5f)
            {
                _rigidbody.position += Vector3.up * jumpSpeed * Time.deltaTime;
            }
            else
                _jumpPhase = JumpPhase.Fall;
        }
        
        if(_jumpPhase is JumpPhase.Fall)
        {
            _rigidbody.position -= Vector3.up * jumpSpeed * Time.deltaTime;
        }

        _rigidbody.rotation = Quaternion.Euler(_rigidbody.rotation.eulerAngles - Vector3.forward * Time.deltaTime * 150f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_isJumping)
            return;

        float zAngle = transform.eulerAngles.z;
        zAngle = Mathf.Round(zAngle / 90f) * 90f;

        _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, Quaternion.Euler(0f, 0f, zAngle), Time.deltaTime * 90f);

        Vector3 position = _rigidbody.position;
        position.y = collision.contacts[0].point.y + transform.lossyScale.y / 2f;
        _rigidbody.position = position;
    }

    private void OnCollisionExit(Collision collision)
    {

    }
}

public enum JumpPhase
{
    Rise,
    Fall
}