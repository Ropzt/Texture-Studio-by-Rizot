using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;

namespace TextureStudio
{
    [CreateAssetMenu(fileName = "TS", menuName = "ScriptableObjects/TS", order = 1)]
    public class TS_Main : ScriptableObject
    {
        //Texture

        //public bool mirrorHorizontal = false;
        //public bool randMirrorHBool = false;
        //public int randMirrorHSeed = 0;
        //private RandomizedProperty randMirrorH;
        //public bool mirrorVertical = false;
        //public bool randMirrorVBool = false;
        //public int randMirrorVSeed = 0;
        //private RandomizedProperty randMirrorV;

        public Texture2D red;
        public Texture2D green;
        public Texture2D blue;
        public Texture2D alpha;

        public Channel redSource = Channel.R;
        public Channel greenSource = Channel.G;
        public Channel blueSource = Channel.B;
        public Channel alphaSource = Channel.A;
        
        public bool invertRed = false;
        public bool invertGreen = false;
        public bool invertBlue = false;
        public bool invertAlpha = false;

        public bool useAlphaMask = true;
        public bool useBilinearFilter = true;
        public bool useAntiAliasing = true;
        public bool useSeamless = true;
        public Vector2Int stampDimensions = new Vector2Int(256, 256);
        public int stampIterations = 1;

        //Rotation
        [Range(-180, 180)] public float rotation = 0;
        public bool randRotationBool = false;
        public int randRotationSeed = 0;
        public Vector2 randRotationRange = Vector2.zero;
        private RandomizedProperty randRot;

        //Displace
        public Vector2 displace = Vector2.zero;
        public float stampRadius = 256f;
        public bool randDisplaceBool = false;
        public int randDisplaceSeed = 0;

        //Scale
        public Vector2 scale = Vector2.one;
        public bool randScaleBool = false;
        public int randScaleSeed = 0;
        public bool useNonUniformScale = false;
        public Vector2 randScaleXRange = Vector2.one;
        public Vector2 randScaleYRange = Vector2.one;
        private RandomizedProperty randScale;

        //Output
        public BackgroundColor outBaseColorEnum = BackgroundColor.Transparent;
        public Color outBaseColor = new Color(1, 1, 1, 1);
        public Texture2D outBaseTexture;
        public Vector2Int outputDimensions = new Vector2Int(256, 256);
        public GraphicsFormat outputFormat = GraphicsFormat.R32G32B32A32_SFloat;
        public string outFileName;
        public SaveTextureFileFormat fileFormat = SaveTextureFileFormat.PNG;

        //Internal
        private RenderTexture stamp;
        private RenderTexture rt;
        public ComputeShader initRT;
        public ComputeShader initRTTexture;
        public ComputeShader stampPacker;
        public ComputeShader stamper;

        public RenderTexture GetStamp
        {
            get { return stamp; }
        }

        public RenderTexture GetRT
        {
            get { return rt; }
        }


        private Vector4 ChannelToVector(Channel channel)
        {
            switch (channel)
            {
                case Channel.R:
                default:
                    return new Vector4(1, 0, 0, 0);
                case Channel.G:
                    return new Vector4(0, 1, 0, 0);
                case Channel.B:
                    return new Vector4(0, 0, 1, 0);
                case Channel.A:
                    return new Vector4(0, 0, 0, 1);
            }
        }

        private Vector4 BackgroundToColor(BackgroundColor backgroundColor)
        {
            switch (backgroundColor)
            {
                case BackgroundColor.Transparent:
                default:
                    return new Vector4(0, 0, 0, 0);
                case BackgroundColor.Black:
                    return new Vector4(0, 0, 0, 1);
                case BackgroundColor.White:
                    return new Vector4(1, 1, 1, 1);
                case BackgroundColor.Color:
                    return outBaseColor;
                case BackgroundColor.Normal:
                    return new Vector4(0.5f, 0.5f, 1, 1);
            }
        }

        void InitRandoms()
        {
            randRot = new RandomizedProperty(randRotationSeed);
            randScale = new RandomizedProperty(randScaleSeed);
        }

        void InitRTs()
        {
            stamp = new RenderTexture(stampDimensions.x, stampDimensions.y, 0, outputFormat);
            stamp.enableRandomWrite = true;
            stamp.wrapMode = TextureWrapMode.Repeat;
            stamp.filterMode = FilterMode.Bilinear;
            stamp.Create();

            rt = new RenderTexture(outputDimensions.x, outputDimensions.y, 0, outputFormat);
            rt.enableRandomWrite = true;
            rt.Create();
        }

        public void Stamp()
        {
            InitRandoms();

            InitRTs();
            if (outBaseColorEnum == BackgroundColor.Texture)
            {
                if (outBaseTexture.IsUnityNull())
                {
                    Debug.LogWarning("Output Background Texture is null. Background set to Transparent.");
                    //Set the RT background
                    outBaseColorEnum = BackgroundColor.Transparent;
                    initRT.SetVector("Color", BackgroundToColor(outBaseColorEnum));
                    initRT.SetTexture(0, "Result", rt);
                    initRT.Dispatch(0, Mathf.FloorToInt((float)outputDimensions.x / 8f),
                        Mathf.FloorToInt((float)outputDimensions.y / 8f), 1);

                }
                else
                {
                    float w = (float)outputDimensions.x / (float)outBaseTexture.width;
                    float h = (float)outputDimensions.y / (float)outBaseTexture.height;

                    initRTTexture.SetFloats("Scale", w, h);
                    initRTTexture.SetTexture(0, "Provided", outBaseTexture);
                    initRTTexture.SetTexture(0, "Result", rt);
                    initRTTexture.Dispatch(0, Mathf.FloorToInt((float)outputDimensions.x / 8f),
                        Mathf.FloorToInt((float)outputDimensions.y / 8f), 1);

                }
            }
            else
            {
                //Set the RT background
                initRT.SetVector("Color", BackgroundToColor(outBaseColorEnum));
                initRT.SetTexture(0, "Result", rt);
                initRT.Dispatch(0, Mathf.FloorToInt((float)outputDimensions.x / 8f),
                    Mathf.FloorToInt((float)outputDimensions.y / 8f), 1);

            }

            //Set up the stamp
            Texture2D[] texs = new Texture2D[4];
            texs[0] = red;
            texs[1] = green;
            texs[2] = blue;
            texs[3] = alpha;

            Channel[] channels = new Channel[4];
            channels[0] = redSource;
            channels[1] = greenSource;
            channels[2] = blueSource;
            channels[3] = alphaSource;

            Channel[] dest = new Channel[4];
            dest[0] = Channel.R;
            dest[1] = Channel.G;
            dest[2] = Channel.B;
            dest[3] = Channel.A;
            
            bool[] bools = new bool[4];
            bools[0] = invertRed;
            bools[1] = invertGreen;
            bools[2] = invertBlue;
            bools[3] = invertAlpha;
            

            for (int i = 0; i < 4; i++)
            {
                if (!texs[i].IsUnityNull())
                {
                    float w = (float)stampDimensions.x / (float)texs[i].width;
                    float h = (float)stampDimensions.y / (float)texs[i].height;

                    stampPacker.SetBool("Invert", bools[i]);
                    stampPacker.SetVector("Destination", ChannelToVector(dest[i]));
                    stampPacker.SetVector("ChannelVector", ChannelToVector(channels[i]));
                    stampPacker.SetFloats("Scale", w, h);
                    stampPacker.SetTexture(0, "InTex", texs[i]);
                    stampPacker.SetTexture(0, "Stamp", stamp);

                    // Scale is added here to allow proper sampling of the stamp (no black bands)
                    stampPacker.Dispatch(0, Mathf.FloorToInt((float)stampDimensions.x / 8f),
                        Mathf.FloorToInt((float)stampDimensions.y / 8f), 1);

                }
            }


            //Stamp
            stamper.SetFloats("StampDimensions", stamp.width, stamp.height);
            stamper.SetFloats("ResultDimensions", rt.width, rt.height);
            stamper.SetFloat("alphaMaskBool",
                useAlphaMask ? 0 : 1); // true = 0 / false = 1  (used in a max() comparison in shader)
            stamper.SetTexture(0, "Stamp", stamp);
            stamper.SetTexture(0, "Result", rt);

            if (stampIterations <= 0)
            {
                stampIterations = 1;
            }

            float rot = rotation;
            Vector2 disp = displace;
            Vector2 sca = scale;

            int iterations = stampIterations;
            List<Vector2Int> poisson = new List<Vector2Int>();

            if (randDisplaceBool)
            {
                poisson = PoissonDiscSampling.GeneratePoints(stampRadius, outputDimensions, randDisplaceSeed);
                if (poisson.Count < iterations)
                {
                    iterations = poisson.Count;
                    Debug.Log("Couldn't find enough positions (" + poisson.Count + ") to match stamp iterations (" +
                              stampIterations + "). Try slightly decreasing the radius.");
                }
            }

            for (int i = 0; i < iterations; i++)
            {
                //Rotation
                if (randRotationBool)
                {
                    rot = randRot.GetRandomValue(randRotationRange.x, randRotationRange.y);
                }

                float radians = rot * ((3.14159274f * 2) / 360f);
                float s = Mathf.Sin(radians);
                float c = Mathf.Cos(radians);

                //Displace
                if (randDisplaceBool)
                {
                    disp = poisson[i];
                }

                //Scale
                if (randScaleBool)
                {
                    if (useNonUniformScale)
                    {
                        sca = new Vector2(randScale.GetRandomValue(randScaleXRange.x, randScaleXRange.y),
                            randScale.GetRandomValue(randScaleYRange.x, randScaleYRange.y));
                    }
                    else
                    {
                        float uni = randScale.GetRandomValue(randScaleXRange.x, randScaleXRange.y);
                        sca = new Vector2(uni, uni);
                    }

                }

                stamper.SetBool("UseBilinearFilter", useBilinearFilter);
                stamper.SetBool("UseAntiAliasing", useAntiAliasing);
                stamper.SetBool("UseSeamless", useSeamless);
                stamper.SetVector("RotationValues", new Vector4(c, -s, s, c));
                stamper.SetFloats("Displace", disp.x, disp.y);
                stamper.SetFloats("Scale", sca.x, sca.y);

                // Scale is added here to allow proper sampling of the stamp (no black bands)
                stamper.Dispatch(0, Mathf.FloorToInt(rt.width / 8f), Mathf.FloorToInt(rt.height / 8f), 1);

            }

            Debug.Log("Texture generated successfully !");
        }

        public void SaveFile()
        {
            Texture2D tex;
            if (fileFormat != SaveTextureFileFormat.EXR)
                tex = new Texture2D(rt.width, rt.height, outputFormat, TextureCreationFlags.None);
            else
                tex = new Texture2D(rt.width, rt.height, GraphicsFormat.R32G32B32A32_SFloat, TextureCreationFlags.None);
            RenderTexture.active = rt;
            tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            tex.Apply();

            switch (fileFormat)
            {
                case SaveTextureFileFormat.EXR:
                    System.IO.File.WriteAllBytes(Application.dataPath + "/" + outFileName + ".exr",
                        tex.EncodeToEXR(Texture2D.EXRFlags.OutputAsFloat));
                    break;
                case SaveTextureFileFormat.JPG:
                    System.IO.File.WriteAllBytes(Application.dataPath + "/" + outFileName + ".jpg",
                        tex.EncodeToJPG(95));
                    break;
                case SaveTextureFileFormat.PNG:
                    System.IO.File.WriteAllBytes(Application.dataPath + "/" + outFileName + ".png", tex.EncodeToPNG());
                    break;
                case SaveTextureFileFormat.TGA:
                    System.IO.File.WriteAllBytes(Application.dataPath + "/" + outFileName + ".tga", tex.EncodeToTGA());
                    break;
            }

            Debug.Log("Texture saved successfully !");

            if (Application.isPlaying)
                Object.Destroy(tex);
            else
                Object.DestroyImmediate(tex);

        }
    }

    [System.Serializable]
    public class RandomizedProperty
    {
        public RandomizedProperty(int seed)
        {
            InitSeed(seed);
        }

        public void InitSeed(int seed)
        {
            Random.InitState(seed);
        }

        public float GetRandomValue(float min, float max)
        {
            return Random.Range(min, max);
        }
    }

    [System.Serializable]
    public enum Channel
    {
        R = 0,
        G = 1,
        B = 2,
        A = 3
    }

    [System.Serializable]
    public enum SaveTextureFileFormat
    {
        EXR,
        JPG,
        PNG,
        TGA
    };

    [System.Serializable]
    public enum BackgroundColor
    {
        Transparent,
        Color,
        Black,
        White,
        Normal,
        Texture
    };
}    