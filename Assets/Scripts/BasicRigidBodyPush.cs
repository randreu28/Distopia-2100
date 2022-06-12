using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
	public LayerMask pushLayers;
	public bool _canPush;
	private Animator _animator;
	private bool _hasAnimator;
	private Inputs _input;
	[Range(5f, 50f)] public float strength = 11f;
	private PullController _pullController;

	[SerializeField]
	private AudioClip _movingSound;

	[SerializeField]
	[Range(0, 1)]
	private float _volume;

	private AudioSource _audioSource;

	private bool _sound;

	private void Start()
    {
		_hasAnimator = TryGetComponent(out _animator);
		_pullController = GetComponent<PullController>();
		_audioSource = GetComponent<AudioSource>();
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
	{

		if (_canPush) PushRigidBodies(hit);
	}

	private void PushRigidBodies(ControllerColliderHit hit)
	{
		// make sure we hit a non kinematic rigidbody
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) return;

		// make sure we only push desired layer(s)
		var bodyLayerMask = 1 << body.gameObject.layer;
		if ((bodyLayerMask & pushLayers.value) == 0) return;

		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) return;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		if (!_audioSource.isPlaying) { 
			_audioSource.Play();
		}

		_pullController.Action = false;

		Debug.Log("Pushing");
		
		
		// Apply the push and take strength into account
		body.AddForce(pushDir * strength * Time.deltaTime, ForceMode.Impulse);

		if (_hasAnimator && pushDir != Vector3.zero)
		{
			_animator.SetBool("Pushing", true);
		}

	}

	public void OnAction()
    {
		//Debug.Log("Action");
		_canPush = true;
		_audioSource.clip = _movingSound;
		_audioSource.volume = _volume;
	}

	public void OnActionEnd()
	{
		//Debug.Log("Action End");
		_canPush = false;
		_audioSource.Stop();
		if (_hasAnimator)
		{
			_animator.SetBool("Pushing", false);
			_animator.SetBool("Pulling", false);
		}
	}

}