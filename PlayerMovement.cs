using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//DEBUG
	public Vector2 moveSpeedDir;
	public float moveSpeed;
	
	//MOVEMENT
	private float horizontal; // Lưu trữ giá trị đầu vào theo phương ngang
	private float speed = 8f; // Tốc độ di chuyển của người chơi
	private float jumpingPower = 16f; // Sức mạnh của nhảy của người chơi

	//SYSTEM
	public Rigidbody2D rb;

	void Update()
	{
		// Lấy giá trị đầu vào phương ngang
		horizontal = Input.GetAxisRaw("Horizontal");

		// Kiểm tra nếu người chơi nhấn nút nhảy và đang ở trên mặt đất, sau đó áp dụng lực nhảy
		if (Input.GetButtonDown("Jump"))
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
		}

		// Nếu nút nhảy được thả ra và người chơi vẫn đang đi lên, giảm lực nhảy
		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}
		
		moveSpeedDir = rb.velocity;
		moveSpeed = moveSpeedDir.magnitude;
	}

	private void FixedUpdate()
	{
		// Di chuyển người chơi theo phương ngang dựa trên đầu vào
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
	}
}
