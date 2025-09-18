struct Boid
{
    float3 position;
    float3 velocity;
    float4 rotation;
};

StructuredBuffer<Boid> boids;

void Instancing_float(
        float3 Position, 
        float3 Normal, 
        float ID, 
        out float3 WorldPosition, 
        out float3 WorldNormal)
{
    Boid b = boids[(uint) ID];

    // Ensure velocity is non-zero
    float3 forward = normalize(b.velocity + 1e-5);
    float3 up = float3(0, 1, 0);

    // Build orthonormal basis
    float3 right = normalize(cross(up, forward));
    up = cross(forward, right);

    float3x3 rot = float3x3(right, up, forward);

    // Rotate & translate vertex
    float3 localPos = mul(rot, Position);
    float3 newPos = localPos + b.position;

    // Rotate normal
    float3 localNormal = mul(rot, Normal);

    WorldPosition = TransformObjectToWorld(newPos);
    WorldNormal = TransformObjectToWorldNormal(localNormal);

}

