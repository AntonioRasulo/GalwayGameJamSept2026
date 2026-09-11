#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

Texture2D SpriteTexture;
float4x4 MatrixTransform;
float2 ScreenSize;
float SpinAmount;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderInput
{
    float4 Position	: POSITION0;
    float4 Color	: COLOR0;
    float2 TexCoord	: TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	return tex2D(SpriteTextureSampler,input.TextureCoordinates) * input.Color;
}

VertexShaderOutput MainVS(VertexShaderInput input) 
{
    VertexShaderOutput output;

    float4 pos = input.Position;

    // create the center of rotation
    float2 centerXZ = float2(ScreenSize.x * .5, 0);

    // convert the debug variable into an angle from 0 to 2 pi. 
    //  shaders use radians for angles, so 2 pi = 360 degrees
    float angle = SpinAmount * 6.28;
    
    // pre-compute the cos and sin of the angle
    float cosA = cos(angle);
    float sinA = sin(angle);
    
    // shift the position to the center of rotation
    pos.xz -= centerXZ;
    
    // compute the rotation
    float nextX = pos.x * cosA - pos.z * sinA;
    float nextZ = pos.x * sinA + pos.z * cosA;
    
    // apply the rotation
    pos.x = nextX;
    pos.z = nextZ;
    
    // shift the position away from the center of rotation
    pos.xz += centerXZ;
    
    output.Position = mul(pos, MatrixTransform);
    output.Color = input.Color;
    output.TextureCoordinates = input.TexCoord;
    return output;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
        VertexShader = compile VS_SHADERMODEL MainVS();
	}
};