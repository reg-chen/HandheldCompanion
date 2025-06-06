﻿using HandheldCompanion.Inputs;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;


namespace HandheldCompanion.Models;

internal class Model8BitDoLite2 : IModel
{
    // Specific groups (move me)
    private readonly Model3DGroup BodyBack;
    private readonly Model3DGroup ChargerConnector;
    private readonly Model3DGroup ChargerPort;
    private readonly Model3DGroup Home;
    private readonly Model3DGroup JoystickLeftCover;
    private readonly Model3DGroup JoystickRightCover;
    private readonly Model3DGroup LED1;
    private readonly Model3DGroup LED2;
    private readonly Model3DGroup LED3;
    private readonly Model3DGroup LED4;
    private readonly Model3DGroup Logo;
    private readonly Model3DGroup Pair;
    private readonly Model3DGroup Power;
    private readonly Model3DGroup Reset;
    private readonly Model3DGroup ShoulderLeftMiddle;
    private readonly Model3DGroup ShoulderRightMiddle;
    private readonly Model3DGroup Star;

    public Model8BitDoLite2() : base("8BitDoLite2")
    {
        // colors
        var ColorPlasticWhite = (Color)ColorConverter.ConvertFromString("#E1E0E6");
        var ColorPlasticBlack = (Color)ColorConverter.ConvertFromString("#43424B");
        var ColorLED = (Color)ColorConverter.ConvertFromString("#487B40");
        var ColorHighlight = (Brush)Application.Current.Resources["AccentButtonBackground"];

        var MaterialPlasticWhite = new DiffuseMaterial(new SolidColorBrush(ColorPlasticWhite));
        var MaterialPlasticBlack = new DiffuseMaterial(new SolidColorBrush(ColorPlasticBlack));
        var MaterialPlasticTransparentLED = new SpecularMaterial(new SolidColorBrush(ColorLED), 0.3);
        var MaterialHighlight = new DiffuseMaterial(ColorHighlight);

        // Rotation Points
        JoystickRotationPointCenterLeftMillimeter = new Vector3D(-38.0f, -2.4f, 1.15f);
        JoystickRotationPointCenterRightMillimeter = new Vector3D(18.6f, -2.4f, -17.2f);
        JoystickMaxAngleDeg = 20.0f;

        ShoulderTriggerRotationPointCenterLeftMillimeter = new Vector3D(-34.00f, 5.0f, 23.2f);
        ShoulderTriggerRotationPointCenterRightMillimeter = new Vector3D(34.00f, 5.0f, 23.2f);
        TriggerMaxAngleDeg = 10.0f;

        UpwardVisibilityRotationAxisLeft = new Vector3D(1, 0, 0);
        UpwardVisibilityRotationAxisRight = new Vector3D(1, 0, 0);
        UpwardVisibilityRotationPointLeft = new Vector3D(-34.90f, -5.0f, 28.0f);
        UpwardVisibilityRotationPointRight = new Vector3D(34.90f, -5.0f, 28.0f);

        // load model(s)
        BodyBack = ModelImporter.Load($"3DModels/{ModelName}/BodyBack.obj");
        ChargerConnector = ModelImporter.Load($"3DModels/{ModelName}/ChargerConnector.obj");
        ChargerPort = ModelImporter.Load($"3DModels/{ModelName}/ChargerPort.obj");
        Logo = ModelImporter.Load($"3DModels/{ModelName}/Logo.obj");
        Home = ModelImporter.Load($"3DModels/{ModelName}/Home.obj");
        JoystickLeftCover = ModelImporter.Load($"3DModels/{ModelName}/JoystickLeftCover.obj");
        JoystickRightCover = ModelImporter.Load($"3DModels/{ModelName}/JoystickRightCover.obj");
        LED1 = ModelImporter.Load($"3DModels/{ModelName}/LED1.obj");
        LED2 = ModelImporter.Load($"3DModels/{ModelName}/LED2.obj");
        LED3 = ModelImporter.Load($"3DModels/{ModelName}/LED3.obj");
        LED4 = ModelImporter.Load($"3DModels/{ModelName}/LED4.obj");
        Pair = ModelImporter.Load($"3DModels/{ModelName}/Pair.obj");
        Power = ModelImporter.Load($"3DModels/{ModelName}/Power.obj");
        Reset = ModelImporter.Load($"3DModels/{ModelName}/Reset.obj");
        Star = ModelImporter.Load($"3DModels/{ModelName}/Star.obj");
        ShoulderRightMiddle = ModelImporter.Load($"3DModels/{ModelName}/Shoulder-Left-Middle.obj");
        ShoulderLeftMiddle = ModelImporter.Load($"3DModels/{ModelName}/Shoulder-Right-Middle.obj");

        // map model(s)
        foreach (ButtonFlags button in Enum.GetValues(typeof(ButtonFlags)))
            switch (button)
            {
                case ButtonFlags.B1:
                case ButtonFlags.B2:
                case ButtonFlags.B3:
                case ButtonFlags.B4:

                    var filename = $"3DModels/{ModelName}/{button}-Symbol.obj";
                    if (File.Exists(filename))
                    {
                        var model = ModelImporter.Load(filename);
                        ButtonMap[button].Add(model);

                        // pull model
                        model3DGroup.Children.Add(model);
                    }

                    break;
            }

        // pull model(s)
        model3DGroup.Children.Add(BodyBack);
        model3DGroup.Children.Add(ChargerConnector);
        model3DGroup.Children.Add(ChargerPort);
        model3DGroup.Children.Add(Logo);
        model3DGroup.Children.Add(Home);
        model3DGroup.Children.Add(JoystickLeftCover);
        model3DGroup.Children.Add(JoystickRightCover);
        model3DGroup.Children.Add(LED1);
        model3DGroup.Children.Add(LED2);
        model3DGroup.Children.Add(LED3);
        model3DGroup.Children.Add(LED4);
        model3DGroup.Children.Add(Pair);
        model3DGroup.Children.Add(Power);
        model3DGroup.Children.Add(Reset);
        model3DGroup.Children.Add(Star);
        model3DGroup.Children.Add(ShoulderLeftMiddle);
        model3DGroup.Children.Add(ShoulderRightMiddle);


        // Colors buttons
        foreach (ButtonFlags button in Enum.GetValues(typeof(ButtonFlags)))
        {
            var i = 0;
            Material buttonMaterial = null;

            if (ButtonMap.TryGetValue(button, out var map))
                foreach (var model3D in map)
                {
                    switch (button)
                    {
                        case ButtonFlags.B1:
                            buttonMaterial = i == 0 ? MaterialPlasticBlack : MaterialPlasticWhite;
                            break;
                        case ButtonFlags.B2:
                            buttonMaterial = i == 0 ? MaterialPlasticBlack : MaterialPlasticWhite;
                            break;
                        case ButtonFlags.B3:
                            buttonMaterial = i == 0 ? MaterialPlasticBlack : MaterialPlasticWhite;
                            break;
                        case ButtonFlags.B4:
                            buttonMaterial = i == 0 ? MaterialPlasticBlack : MaterialPlasticWhite;
                            break;
                        case ButtonFlags.Start:
                        case ButtonFlags.Back:
                            buttonMaterial = MaterialPlasticWhite;
                            break;
                        default:
                            buttonMaterial = MaterialPlasticBlack;
                            break;
                    }

                    DefaultMaterials[model3D] = buttonMaterial;
                    ((GeometryModel3D)model3D.Children[0]).Material = buttonMaterial;

                    i++;
                }
        }

        // Colors models
        foreach (Model3DGroup model3D in model3DGroup.Children)
        {
            if (DefaultMaterials.ContainsKey(model3D))
                continue;

            // generic material(s)
            ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticBlack;
            ((GeometryModel3D)model3D.Children[0]).BackMaterial = MaterialPlasticBlack;
            DefaultMaterials[model3D] = MaterialPlasticBlack;

            // specific material(s)
            if (model3D.Equals(MainBody) || model3D.Equals(BodyBack)
                                         || model3D.Equals(LeftMotor) || model3D.Equals(RightMotor)
                                         || model3D.Equals(Power)
                                         || model3D.Equals(ShoulderLeftMiddle) || model3D.Equals(ShoulderRightMiddle))
            {
                ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticWhite;
                DefaultMaterials[model3D] = MaterialPlasticWhite;
                continue;
            }

            if (model3D.Equals(LED1))
            {
                ((GeometryModel3D)model3D.Children[0]).Material = MaterialPlasticTransparentLED;
                ((GeometryModel3D)model3D.Children[0]).BackMaterial = MaterialPlasticTransparentLED;
                DefaultMaterials[model3D] = MaterialPlasticTransparentLED;
            }
        }

        base.DrawAccentHighligths();
    }
}