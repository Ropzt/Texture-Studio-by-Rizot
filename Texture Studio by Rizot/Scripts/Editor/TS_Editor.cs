
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace TextureStudio
{


    public class TS_Editor : EditorWindow
    {
        public TS_Main ts;
        Vector2 scroll;

        [MenuItem("Tools/Texture Studio")]
        public static void Open() => GetWindow<TS_Editor>("Texture Studio");

        public void OnGUI()
        {
            scroll = GUILayout.BeginScrollView(scroll);
            //Stamp
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Stamp Packer", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);
            EditorGUILayout.BeginHorizontal();

            GUIStyle style = new GUIStyle(GUI.skin.label); // Style for labels
            style.alignment = TextAnchor.UpperLeft;

            //Red
            EditorGUILayout.BeginVertical(GUI.skin.textArea, GUILayout.Width(120));
            GUILayout.Label("Red Channel", style);
            EditorGUI.BeginChangeCheck();
            Texture2D r = (Texture2D)EditorGUILayout.ObjectField(ts.red, typeof(Texture2D), false, GUILayout.Width(100),
                GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Red Channel Texture Input");
                ts.red = r;
            }
            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Source Channel :", GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            var rsour = EditorGUILayout.EnumPopup(ts.redSource, GUILayout.Width(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Red Channel Source");
                ts.redSource = (Channel)rsour;
            }
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Invert",GUILayout.Width(65));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool invR = EditorGUILayout.Toggle(ts.invertRed, GUILayout.Width(25));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Invert Red Channel Texture");
                ts.invertRed = invR;
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();


            //Green
            EditorGUILayout.BeginVertical(GUI.skin.textArea, GUILayout.Width(120));
            GUILayout.Label("Green Channel", style);
            EditorGUI.BeginChangeCheck();
            Texture2D g = (Texture2D)EditorGUILayout.ObjectField(ts.green, typeof(Texture2D), false,
                GUILayout.Width(100), GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Green Channel Texture Input");
                ts.green = g;
            }

            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Source Channel :", GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            var gsour = EditorGUILayout.EnumPopup(ts.greenSource, GUILayout.Width(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Green Channel Source");
                ts.greenSource = (Channel)gsour;
            }
            EditorGUILayout.Space(3);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Invert",GUILayout.Width(65));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool invG = EditorGUILayout.Toggle(ts.invertGreen, GUILayout.Width(25));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Invert Green Channel Texture");
                ts.invertGreen = invG;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            //Blue
            EditorGUILayout.BeginVertical(GUI.skin.textArea, GUILayout.Width(120));
            GUILayout.Label("Blue Channel", style);
            EditorGUI.BeginChangeCheck();
            Texture2D b = (Texture2D)EditorGUILayout.ObjectField(ts.blue, typeof(Texture2D), false,
                GUILayout.Width(100), GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Blue Channel Texture Input");
                ts.blue = b;
            }

            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Source Channel :", GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            var bsour = EditorGUILayout.EnumPopup(ts.blueSource, GUILayout.Width(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Blue Channel Source");
                ts.blueSource = (Channel)bsour;
            }
            EditorGUILayout.Space(3);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Invert",GUILayout.Width(65));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool invB = EditorGUILayout.Toggle(ts.invertBlue,GUILayout.Width(25));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Invert Blue Channel Texture");
                ts.invertBlue = invB;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            //Alpha
            EditorGUILayout.BeginVertical(GUI.skin.textArea, GUILayout.Width(120));
            GUILayout.Label("Alpha Channel", style);
            EditorGUI.BeginChangeCheck();
            Texture2D a = (Texture2D)EditorGUILayout.ObjectField(ts.alpha, typeof(Texture2D), false,
                GUILayout.Width(100), GUILayout.Height(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Alpha Channel Texture Input");
                ts.alpha = a;
            }

            EditorGUILayout.Space(3);

            EditorGUILayout.LabelField("Source Channel :", GUILayout.Width(100));
            EditorGUI.BeginChangeCheck();
            var asour = EditorGUILayout.EnumPopup(ts.alphaSource, GUILayout.Width(100));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Alpha Channel Source");
                ts.alphaSource = (Channel)asour;
            }
            EditorGUILayout.Space(3);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Invert",GUILayout.Width(65));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool invA = EditorGUILayout.Toggle(ts.invertAlpha,GUILayout.Width(25));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Invert Alpha Channel Texture");
                ts.invertAlpha = invA;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Alpha Channel as Stamping Mask", GUILayout.Width(250));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolMask = EditorGUILayout.Toggle(ts.useAlphaMask);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Alpha as Mask");
                ts.useAlphaMask = boolMask;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Bilinear Filtering", GUILayout.Width(250));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolFil = EditorGUILayout.Toggle(ts.useBilinearFilter);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Bilinear Filtering");
                ts.useBilinearFilter = boolFil;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Anti-Alisasing", GUILayout.Width(250));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolAli = EditorGUILayout.Toggle(ts.useAntiAliasing);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Anti-Alisasing");
                ts.useAntiAliasing = boolAli;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Use Seamless", GUILayout.Width(250));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolSeam = EditorGUILayout.Toggle(ts.useSeamless);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Seamless");
                ts.useSeamless = boolSeam;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);



            EditorGUI.BeginChangeCheck();
            Vector2Int stdim = EditorGUILayout.Vector2IntField("Packed Stamp Dimensions", ts.stampDimensions);
            if (stdim.x < 8)
            {
                stdim.x = 8;
            } // to prevent numthreads <1

            if (stdim.y < 8)
            {
                stdim.y = 8;
            } // to prevent numthreads <1

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Packed Stamp Dimensions");
                ts.stampDimensions = stdim;
            }

            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stamp Iterations", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            int iter = EditorGUILayout.IntField(ts.stampIterations);
            if (iter < 1)
            {
                iter = 1;
            } // Makes no sense otherwise

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Stamp Iterations");
                ts.stampIterations = iter;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Displace
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Displacement", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Randomize Displacement", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolDisp = EditorGUILayout.Toggle(ts.randDisplaceBool);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Randomize Displacement");
                ts.randDisplaceBool = boolDisp;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            if (ts.randDisplaceBool)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Stamp Radius", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                float rad = EditorGUILayout.FloatField(ts.stampRadius);
                if (rad < 1)
                {
                    rad = 1;
                }

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Stamp Radius");
                    ts.stampRadius = rad;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Randomized Displacement Seed", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                int tDseed = EditorGUILayout.IntField(ts.randDisplaceSeed);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Displacement Seed");
                    ts.randDisplaceSeed = tDseed;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                Vector2 dis = EditorGUILayout.Vector2Field("Displacement", ts.displace);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Displacement");
                    ts.displace = dis;
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Rotation
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Rotation", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Randomize Rotation", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolRot = EditorGUILayout.Toggle(ts.randRotationBool);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Randomize Rotation");
                ts.randRotationBool = boolRot;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            if (ts.randRotationBool)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Randomized Rotation Range", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                float rX = ts.randRotationRange.x;
                float rY = ts.randRotationRange.y;
                EditorGUI.BeginChangeCheck();
                rX = EditorGUILayout.DelayedFloatField(rX, GUILayout.MinWidth(25), GUILayout.MaxWidth(50));
                EditorGUILayout.MinMaxSlider(ref rX, ref rY, -180f, 180f, GUILayout.MinWidth(100));
                rY = EditorGUILayout.DelayedFloatField(rY, GUILayout.MinWidth(25), GUILayout.MaxWidth(50));
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Rotation Range");
                    ts.randRotationRange.x = rX;
                    ts.randRotationRange.y = rY;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Randomized Rotation Seed", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                int tRseed = EditorGUILayout.IntField(ts.randRotationSeed);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Rotation Seed");
                    ts.randRotationSeed = tRseed;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Rotation Angle", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                float rotAng = EditorGUILayout.Slider(ts.rotation, -180, 180);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Rotation Angle");
                    ts.rotation = rotAng;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Scale
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Scale", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Randomize Scale", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            bool boolSca = EditorGUILayout.Toggle(ts.randScaleBool);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "(Un)Toggle Randomize Scale");
                ts.randScaleBool = boolSca;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            if (ts.randScaleBool)
            {
                if (ts.useNonUniformScale)
                {
                    EditorGUI.BeginChangeCheck();
                    Vector2 scaX = EditorGUILayout.Vector2Field("Randomized Scale X Range", ts.randScaleXRange);
                    //if(scaX.x < 0.001f){ scaX.x =  0.001f; } // prevent division by zero
                    //if(scaX.y < 0.001f){ scaX.y =  0.001f; } // prevent division by zero
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(ts, "Change Scale X Range");
                        ts.randScaleXRange = scaX;
                    }

                    EditorGUILayout.Space(3);

                    EditorGUI.BeginChangeCheck();
                    Vector2 scaY = EditorGUILayout.Vector2Field("Randomized Scale Y Range", ts.randScaleYRange);
                    //if(scaY.x < 0.001f){ scaY.x =  0.001f; } // prevent division by zero
                    //if(scaY.y < 0.001f){ scaY.y =  0.001f; } // prevent division by zero
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(ts, "Change Scale Y Range");
                        ts.randScaleYRange = scaY;
                    }

                    EditorGUILayout.Space(3);
                }
                else
                {
                    EditorGUI.BeginChangeCheck();
                    Vector2 scaX = EditorGUILayout.Vector2Field("Randomized Uniform Scale Range", ts.randScaleXRange);
                    //if(scaX.x < 0.001f){ scaX.x =  0.001f; } // prevent division by zero
                    //if(scaX.y < 0.001f){ scaX.y =  0.001f; } // prevent division by zero
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(ts, "Change Uniform Scale Range");
                        ts.randScaleXRange = scaX;
                    }

                    EditorGUILayout.Space(3);
                }


                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Non-Uniform Scaling", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                bool boolUni = EditorGUILayout.Toggle(ts.useNonUniformScale);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "(Un)Toggle Non-Uniform Scaling");
                    ts.useNonUniformScale = boolUni;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Randomized Scale Seed", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                int tSseed = EditorGUILayout.IntField(ts.randScaleSeed);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Scale Seed");
                    ts.randScaleSeed = tSseed;
                }

                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                Vector2 sca = EditorGUILayout.Vector2Field("Scale", ts.scale);
                //if(sca.x < 0.001f){ sca.x =  0.001f; } // prevent division by zero
                //if(sca.y < 0.001f){ sca.y =  0.001f; } // prevent division by zero
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Scale");
                    ts.scale = sca;
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Output
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Output", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Output Texture Background", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            var backEnum = EditorGUILayout.EnumPopup(ts.outBaseColorEnum);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Output Texture Background");
                ts.outBaseColorEnum = (BackgroundColor)backEnum;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            if (ts.outBaseColorEnum == BackgroundColor.Color)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Output Texture Background Color", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                Color col = EditorGUILayout.ColorField(ts.outBaseColor);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Output Texture Background Color");
                    ts.outBaseColor = col;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);
            }
            else if (ts.outBaseColorEnum == BackgroundColor.Texture)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Output Texture Background Texture", GUILayout.Width(200));
                EditorGUILayout.Space(5);
                EditorGUI.BeginChangeCheck();
                Texture2D tx = (Texture2D)EditorGUILayout.ObjectField(ts.outBaseTexture, typeof(Texture2D), false,
                    GUILayout.Width(100), GUILayout.Height(100));
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(ts, "Change Output Texture Background Texture");
                    ts.outBaseTexture = tx;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(3);
            }

            EditorGUI.BeginChangeCheck();
            Vector2Int outdim = EditorGUILayout.Vector2IntField("Output Texture Dimensions", ts.outputDimensions);
            if (outdim.x < 8)
            {
                outdim.x = 8;
            } // I think 8 is sufficient but can be changed down to 1

            if (outdim.y < 8)
            {
                outdim.y = 8;
            } // I think 8 is sufficient but can be changed down to 1

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Output Texture Dimensions");
                ts.outputDimensions = outdim;
            }

            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Output Texture Format", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            var outFormat = EditorGUILayout.EnumPopup(ts.outputFormat);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Output Texture Format");
                ts.outputFormat = (GraphicsFormat)outFormat;

            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Output File Name", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            string fName = EditorGUILayout.TextField(ts.outFileName);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Output File Name");
                ts.outFileName = fName;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(3);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Output File Format", GUILayout.Width(200));
            EditorGUILayout.Space(5);
            EditorGUI.BeginChangeCheck();
            var fFormat = EditorGUILayout.EnumPopup(ts.fileFormat);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(ts, "Change Output File Format");
                ts.fileFormat = (SaveTextureFileFormat)fFormat;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Buttons
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Preview Result"))
            {
                ts.Stamp();
            }

            EditorGUILayout.Space(10);

            if (GUILayout.Button("Save Result as File"))
            {
                ts.SaveFile();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(15);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);

            //Previews
            EditorGUILayout.BeginVertical(GUI.skin.textArea);
            EditorGUILayout.LabelField("Preview", EditorStyles.whiteBoldLabel);
            EditorGUILayout.Space(7);
            EditorGUILayout.BeginHorizontal();

            if (!ts.GetStamp.IsUnityNull())
            {
                GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Stamp");
                GUILayout.Box(ts.GetStamp, GUILayout.Height(300), GUILayout.Width(300));
                GUILayout.EndVertical();
            }

            if (!ts.GetRT.IsUnityNull())
            {
                GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Result");
                GUILayout.Box(ts.GetRT, GUILayout.Height(300), GUILayout.Width(300));
                GUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            GUILayout.EndScrollView();

        }
    }
}