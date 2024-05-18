using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    private static BallsManager _instance;
    public static BallsManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        Balls = new List<Ball>();
    }

    #endregion

    [SerializeField]
    private Ball ballPrefab;

    private Ball initialBall;
    private Rigidbody2D initialBallRb;

    public float initialBallSpeed = 250f;

    public List<Ball> Balls { get; set; }
    public Rigidbody2D InitialBallRb
    {
        get => initialBallRb;
        set => initialBallRb = value;
    }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.transform.position;
            Vector3 ballPosition = new(paddlePosition.x, paddlePosition.y + 0.5f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0))
            {
                InitialBallRb.isKinematic = false;
                InitialBallRb.AddForce(new Vector2(0, initialBallSpeed));
                GameManager.Instance.IsGameStarted = true;
            }
        }
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.transform.position;
        Vector3 startingPosition = new(paddlePosition.x, paddlePosition.y + 0.5f, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        Balls.Add(initialBall);
    }
}
