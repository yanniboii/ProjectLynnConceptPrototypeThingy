using UnityEngine;

public class BoidMaster : MonoBehaviour
{
    [SerializeField] private ComputeShader boidComputeShader;
    [SerializeField] private int boidAmount;

    [SerializeField] private Vector3 boundsExtents = new Vector3(20, 10, 20);
    [SerializeField] private float boundsRadius;
    [SerializeField] private Vector3 boundsCenter = Vector3.zero;

    [SerializeField] private Transform followTarget;
    [SerializeField] private Transform avoidTarget;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float avoidanceRadius;
    [SerializeField] private float cohesionRadius;
    [SerializeField] private float alignmentRadius;

    [Range(0, 10)]
    [SerializeField] private float boundsStrength;
    [Range(0, 10)]
    [SerializeField] private float followStrength;
    [Range(0, 10)]
    [SerializeField] private float avoidStrength;
    [Range(0, 10)]
    [SerializeField] private float avoidanceStrength;
    [Range(0, 10)]
    [SerializeField] private float cohesionStrength;
    [Range(0, 10)]
    [SerializeField] private float alignmentStrength;

    private ComputeBuffer boidComputeBuffer;
    private BoidData[] cpuData;

    private float seed;
    private bool boidsInitialized;

    private int check = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boidComputeShader = Instantiate(boidComputeShader);

        seed = Random.Range(0, 10000);
        CreateBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        Dispatch();
    }

    private void OnDestroy()
    {
        boidComputeBuffer.Dispose();
    }

    void CreateBuffer()
    {
        boidComputeBuffer = new ComputeBuffer(boidAmount, (sizeof(float) * 10));
    }

    void SetBuffer()
    {
        boidComputeShader.SetBuffer(0, "Boids", boidComputeBuffer);
    }

    void SetData()
    {
        SetBuffer();

        boidComputeShader.SetFloat("deltaTime", Time.deltaTime);
        boidComputeShader.SetFloat("time", Time.time);

        boidComputeShader.SetInt("boidAmount", boidAmount);

        boidComputeShader.SetVector("boundsExtents", boundsExtents);
        boidComputeShader.SetFloat("boundsRadius", boundsRadius);
        boidComputeShader.SetVector("boundsCenter", boundsCenter);

        boidComputeShader.SetVector("followTarget", followTarget.position);
        boidComputeShader.SetFloat("followStrength", followStrength);

        boidComputeShader.SetVector("avoidTarget", avoidTarget.position);
        boidComputeShader.SetFloat("avoidStrength", avoidStrength);

        boidComputeShader.SetFloat("forwardSpeed", forwardSpeed);
        boidComputeShader.SetFloat("rotationSpeed", rotationSpeed);

        boidComputeShader.SetFloat("avoidanceRadius", avoidanceRadius);
        boidComputeShader.SetFloat("cohesionRadius", cohesionRadius);
        boidComputeShader.SetFloat("alignmentRadius", alignmentRadius);

        boidComputeShader.SetFloat("boundsStrength", boundsStrength);
        boidComputeShader.SetFloat("avoidanceStrength", avoidanceStrength);
        boidComputeShader.SetFloat("cohesionStrength", cohesionStrength);
        boidComputeShader.SetFloat("alignmentStrength", alignmentStrength);

        boidComputeShader.SetFloat("seed", seed);
        boidComputeShader.SetBool("boidsInitialized", boidsInitialized);
    }

    void GetData()
    {
        if (cpuData == null)
            cpuData = new BoidData[boidAmount];

        if (cpuData != null)
            boidsInitialized = true;

        //boidComputeBuffer.GetData(cpuData);
    }

    void Dispatch()
    {
        SetData();

        int numThreads = 128;
        int groups = Mathf.CeilToInt((float)boidAmount / (float)numThreads);

        boidComputeShader.Dispatch(0, groups, 1, 1);

        //GetData();
        //if (boidsInitialized && check < 1)
        //{
        //    for (int i = 0; i < 2; i++)
        //    {
        //        Debug.Log($"Boid {i}: \nPos={cpuData[i].pos}, Vel={cpuData[i].velocity}");
        //    }
        //    check++;
        //}

        boidsInitialized = true;
    }

    public int GetBoidAmount() { return boidAmount; }
    public ComputeBuffer GetBoidComputeBuffer() { return boidComputeBuffer; }
    public Vector3 GetBoundsCenter() { return boundsCenter; }
    public Vector3 GetBoundsExtents() { return boundsExtents; }
}


struct BoidData
{
    public Vector3 pos;
    public Vector3 velocity;
    public Vector4 rotation;
}