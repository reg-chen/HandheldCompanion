using HandheldCompanion.Inputs;
using System;
using System.Windows.Media;
using System.Windows.Media.Media3D;


namespace HandheldCompanion.Models;

internal class ModelXBOXOne : IModel
{
    private readonly Model3DGroup B1Button;

    private readonly Model3DGroup B1Interior;
    private readonly Model3DGroup B1Interior2;
    private readonly Model3DGroup B2Button;
    private readonly Model3DGroup B2Interior;
    private readonly Model3DGroup B2Interior2;
    private readonly Model3DGroup B3Button;
    private readonly Model3DGroup B3Interior;
    private readonly Model3DGroup B3Interior2;
    private readonly Model3DGroup B4Button;
    private readonly Model3DGroup B4Interior;

    private readonly Model3DGroup B4Interior2;

    // Specific models
    private readonly Model3DGroup BackSymbol;
    private readonly Model3DGroup BatteryDoor;
    private readonly Model3DGroup BatteryDoorInner;
    private readonly Model3DGroup MainBodyBack;
    private readonly Model3DGroup MainBodySide;
    private readonly Model3DGroup MainBodyTop;
    private readonly Model3DGroup ShareButton;
    private readonly Model3DGroup ShareButtonSymbol;
    private readonly Model3DGroup SpecialOuter;
    private readonly Model3DGroup StartSymbol;
    private readonly Model3DGroup USBPortInner;
    private readonly Model3DGroup USBPortOuter;

    public ModelXBOXOne() : base("XBOXOne")
    {
        // colors
        var ColorPlasticBlack = (Color)ColorConverter.ConvertFromString("#26272C");
        var ColorPlasticWhite = (Color)ColorConverter.ConvertFromString("#D8D7DC");

        var ColorPlasticYellow = (Color)ColorConverter.ConvertFromString("#E4D70E");
        var ColorPlasticGreen = (Color)ColorConverter.ConvertFromString("#76BA58");
        var ColorPlasticRed = (Color)ColorConverter.ConvertFromString("#FA3D45");
        var ColorPlasticBlue = (Color)ColorConverter.ConvertFromString("#119AE5");

        var ColorPlasticTransparent = (Color)ColorConverter.ConvertFromString("#232323");
        ColorPlasticTransparent.A = 50;

        // materials
        var MaterialPlasticBlack = new DiffuseMaterial(new SolidColorBrush(ColorPlasticBlack));
        var MaterialPlasticWhite = new DiffuseMaterial(new SolidColorBrush(ColorPlasticWhite));

        var MaterialPlasticYellow = new DiffuseMaterial(new SolidColorBrush(ColorPlasticYellow));
        var MaterialPlasticGreen = new DiffuseMaterial(new SolidColorBrush(ColorPlasticGreen));
        var MaterialPlasticRed = new DiffuseMaterial(new SolidColorBrush(ColorPlasticRed));
        var MaterialPlasticBlue = new DiffuseMaterial(new SolidColorBrush(ColorPlasticBlue));

        var MaterialPlasticTransparent = new DiffuseMaterial(new SolidColorBrush(ColorPlasticTransparent));

        // rotation Points
        JoystickRotationPointCenterLeftMillimeter = new Vector3D(-39.0f, -8.0f, 22.2f);
        JoystickRotationPointCenterRightMillimeter = new Vector3D(20.0f, -8.0f, -1.1f);
        JoystickMaxAngleDeg = 17.0f;

        ShoulderTriggerRotationPointCenterLeftMillimeter = new Vector3D(-44.668f, 3.087f, 39.705);
        ShoulderTriggerRotationPointCenterRightMillimeter = new Vector3D(44.668f, 3.087f, 39.705);
        TriggerMaxAngleDeg = 16.0f;

        UpwardVisibilityRotationAxisLeft = new Vector3D(1, 0, 0);
        UpwardVisibilityRotationAxisRight = new Vector3D(1, 0, 0);
        UpwardVisibilityRotationPointLeft = new Vector3D(-28.7f, -20.3f, 52.8f);
        UpwardVisibilityRotationPointRight = new Vector3D(28.7f, -20.3f, 52.8f);

        // load models
        BackSymbol = ModelImporter.Load($"3DModels/{ModelName}/BackSymbol.obj");
        BatteryDoor = ModelImporter.Load($"3DModels/{ModelName}/BatteryDoor.obj");
        BatteryDoorInner = ModelImporter.Load($"3DModels/{ModelName}/BatteryDoorInner.obj");
        SpecialOuter = ModelImporter.Load($"3DModels/{ModelName}/SpecialOuter.obj");
        MainBodyBack = ModelImporter.Load($"3DModels/{ModelName}/MainBodyBack.obj");
        MainBodyTop = ModelImporter.Load($"3DModels/{ModelName}/MainBodyTop.obj");
        MainBodySide = ModelImporter.Load($"3DModels/{ModelName}/MainBodySide.obj");
        ShareButton = ModelImporter.Load($"3DModels/{ModelName}/ShareButton.obj");
        ShareButtonSymbol = ModelImporter.Load($"3DModels/{ModelName}/ShareButtonSymbol.obj");
        StartSymbol = ModelImporter.Load($"3DModels/{ModelName}/StartSymbol.obj");
        USBPortInner = ModelImporter.Load($"3DModels/{ModelName}/USBPortInner.obj");
        USBPortOuter = ModelImporter.Load($"3DModels/{ModelName}/USBPortOuter.obj");

        B1Interior = ModelImporter.Load($"3DModels/{ModelName}/B1-Interior.obj");
        B1Interior2 = ModelImporter.Load($"3DModels/{ModelName}/B1-Interior2.obj");
        B1Button = ModelImporter.Load($"3DModels/{ModelName}/B1-Button.obj");

        B2Interior = ModelImporter.Load($"3DModels/{ModelName}/B2-Interior.obj");
        B2Interior2 = ModelImporter.Load($"3DModels/{ModelName}/B2-Interior2.obj");
        B2Button = ModelImporter.Load($"3DModels/{ModelName}/B2-Button.obj");

        B3Interior = ModelImporter.Load($"3DModels/{ModelName}/B3-Interior.obj");
        B3Interior2 = ModelImporter.Load($"3DModels/{ModelName}/B3-Interior2.obj");
        B3Button = ModelImporter.Load($"3DModels/{ModelName}/B3-Button.obj");

        B4Interior = ModelImporter.Load($"3DModels/{ModelName}/B4-Interior.obj");
        B4Interior2 = ModelImporter.Load($"3DModels/{ModelName}/B4-Interior2.obj");
        B4Button = ModelImporter.Load($"3DModels/{ModelName}/B4-Button.obj");

        /*
         * TODO: @CasperH can you please help me figure out which object is the xbox button and rename it to Special.obj
         * See XBOX360 changes I made, I believe they're fine
         */

        // pull models
        model3DGroup.Children.Add(BackSymbol);
        model3DGroup.Children.Add(BatteryDoor);
        model3DGroup.Children.Add(BatteryDoorInner);
        model3DGroup.Children.Add(SpecialOuter);
        model3DGroup.Children.Add(MainBodyBack);
        model3DGroup.Children.Add(MainBodyTop);
        model3DGroup.Children.Add(MainBodySide);
        model3DGroup.Children.Add(ShareButton);
        model3DGroup.Children.Add(ShareButtonSymbol);
        model3DGroup.Children.Add(StartSymbol);
        model3DGroup.Children.Add(USBPortInner);
        model3DGroup.Children.Add(USBPortOuter);

        model3DGroup.Children.Add(B1Interior);
        model3DGroup.Children.Add(B1Interior2);
        model3DGroup.Children.Add(B1Button);
        model3DGroup.Children.Add(B2Interior);
        model3DGroup.Children.Add(B2Interior2);
        model3DGroup.Children.Add(B2Button);
        model3DGroup.Children.Add(B3Interior);
        model3DGroup.Children.Add(B3Interior2);
        model3DGroup.Children.Add(B3Button);
        model3DGroup.Children.Add(B4Interior);
        model3DGroup.Children.Add(B4Interior2);
        model3DGroup.Children.Add(B4Button);

        // specific button material(s)
        foreach (ButtonFlags button in Enum.GetValues(typeof(ButtonFlags)))
        {
            Material buttonMaterial = null;

            if (ButtonMap.TryGetValue(button, out var map))
                foreach (var model3D in map)
                {
                    switch (button)
                    {
                        case ButtonFlags.Back:
                        case ButtonFlags.Start:
                            buttonMaterial = MaterialPlasticWhite;
                            break;
                        case ButtonFlags.B1:
                            buttonMaterial = MaterialPlasticGreen;
                            break;
                        case ButtonFlags.B2:
                            buttonMaterial = MaterialPlasticRed;
                            break;
                        case ButtonFlags.B3:
                            buttonMaterial = MaterialPlasticBlue;
                            break;
                        case ButtonFlags.B4:
                            buttonMaterial = MaterialPlasticYellow;
                            break;
                        default:
                            buttonMaterial = MaterialPlasticBlack;
                            break;
                    }

                    DefaultMaterials[model3D] = buttonMaterial;
                    ((GeometryModel3D)model3D.Children[0]).Material = buttonMaterial;
                    ((GeometryModel3D)model3D.Children[0]).BackMaterial = buttonMaterial;
                }
        }

        // materials
        foreach (Model3DGroup model3D in model3DGroup.Children)
        {
            if (DefaultMaterials.ContainsKey(model3D))
                continue;

            // default material(s)
            ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticWhite;
            ((GeometryModel3D)model3D.Children[0]).BackMaterial = MaterialPlasticBlack;
            DefaultMaterials[model3D] = MaterialPlasticWhite;

            // specific material(s)
            if (model3D.Equals(USBPortOuter)
                || model3D.Equals(B1Interior) || model3D.Equals(B1Interior2)
                || model3D.Equals(B2Interior) || model3D.Equals(B2Interior2)
                || model3D.Equals(B3Interior) || model3D.Equals(B3Interior2)
                || model3D.Equals(B4Interior) || model3D.Equals(B4Interior2)
                || model3D.Equals(RightThumbRing) || model3D.Equals(LeftThumbRing)
                || model3D.Equals(LeftShoulderTrigger) || model3D.Equals(RightShoulderTrigger)
                || model3D.Equals(ShareButtonSymbol) || model3D.Equals(StartSymbol) || model3D.Equals(BackSymbol))
            {
                ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticBlack;
                ((GeometryModel3D)model3D.Children[0]).BackMaterial = MaterialPlasticBlack;
                DefaultMaterials[model3D] = MaterialPlasticBlack;
                continue;
            }

            // specific face button material
            if (model3D.Equals(B1Button) || model3D.Equals(B2Button) || model3D.Equals(B3Button) ||
                model3D.Equals(B4Button))
            {
                ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticTransparent;
                ((GeometryModel3D)model3D.Children[0]).BackMaterial = MaterialPlasticTransparent;
            }
        }

        base.DrawAccentHighligths();
    }
}