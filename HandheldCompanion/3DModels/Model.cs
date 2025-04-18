using HandheldCompanion.Inputs;
using HelixToolkit.Wpf;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Application = System.Windows.Application;
using Color = System.Drawing.Color;

namespace HandheldCompanion;

public abstract class IModel : IDisposable
{
    public static ModelImporter ModelImporter = new();

    public ConcurrentDictionary<ButtonFlags, List<Model3DGroup>> ButtonMap = new();

    // Materials
    public Dictionary<Model3DGroup, Material> DefaultMaterials = [];
    public Dictionary<Model3DGroup, Material> HighlightMaterials = [];
    public float JoystickMaxAngleDeg;

    // Rotation Points
    public Vector3D JoystickRotationPointCenterLeftMillimeter;
    public Vector3D JoystickRotationPointCenterRightMillimeter;
    public Model3DGroup LeftMotor;
    public Model3DGroup LeftShoulderTrigger;

    // Common groups
    public Model3DGroup LeftThumb;
    public Model3DGroup LeftThumbRing;

    public Model3DGroup MainBody;

    // Model3D vars
    public Model3DGroup model3DGroup = new();

    public string ModelName;
    public Model3DGroup RightMotor;
    public Model3DGroup RightShoulderTrigger;
    public Model3DGroup RightThumb;
    public Model3DGroup RightThumbRing;
    public Vector3D ShoulderTriggerRotationPointCenterLeftMillimeter;
    public Vector3D ShoulderTriggerRotationPointCenterRightMillimeter;
    public float TriggerMaxAngleDeg;
    public Vector3D UpwardVisibilityRotationAxisLeft;
    public Vector3D UpwardVisibilityRotationAxisRight;
    public Vector3D UpwardVisibilityRotationPointLeft;
    public Vector3D UpwardVisibilityRotationPointRight;

    protected IModel(string ModelName)
    {
        this.ModelName = ModelName;

        // load model(s)
        LeftThumbRing = ModelImporter.Load($"3DModels/{ModelName}/Joystick-Left-Ring.obj");
        RightThumbRing = ModelImporter.Load($"3DModels/{ModelName}/Joystick-Right-Ring.obj");
        LeftMotor = ModelImporter.Load($"3DModels/{ModelName}/MotorLeft.obj");
        RightMotor = ModelImporter.Load($"3DModels/{ModelName}/MotorRight.obj");
        MainBody = ModelImporter.Load($"3DModels/{ModelName}/MainBody.obj");
        LeftShoulderTrigger = ModelImporter.Load($"3DModels/{ModelName}/Shoulder-Left-Trigger.obj");
        RightShoulderTrigger = ModelImporter.Load($"3DModels/{ModelName}/Shoulder-Right-Trigger.obj");

        // map model(s)
        foreach (ButtonFlags button in Enum.GetValues(typeof(ButtonFlags)))
        {
            var filename = $"3DModels/{ModelName}/{button}.obj";
            if (File.Exists(filename))
            {
                var model = ModelImporter.Load(filename);
                ButtonMap.TryAdd(button, [model]);

                switch (button)
                {
                    // specific case, being both a button and a trigger
                    case ButtonFlags.LeftStickClick:
                        LeftThumb = model;
                        break;
                    case ButtonFlags.RightStickClick:
                        RightThumb = model;
                        break;
                }

                // pull model
                model3DGroup.Children.Add(model);
            }
        }

        // pull model(s)
        model3DGroup.Children.Add(LeftThumbRing);
        model3DGroup.Children.Add(RightThumbRing);
        model3DGroup.Children.Add(LeftMotor);
        model3DGroup.Children.Add(RightMotor);
        model3DGroup.Children.Add(MainBody);
        model3DGroup.Children.Add(LeftShoulderTrigger);
        model3DGroup.Children.Add(RightShoulderTrigger);
    }

    protected virtual void DrawHighligths()
    {
        foreach (Model3DGroup model3D in model3DGroup.Children)
        {
            var material = DefaultMaterials[model3D];
            if (material is not DiffuseMaterial)
                continue;

            // determine colors from brush from materials
            var DefaultMaterialBrush = ((DiffuseMaterial)material).Brush;
            var StartColor = ((SolidColorBrush)DefaultMaterialBrush).Color;

            // generic material(s)
            var drawingColor =
                ControlPaint.LightLight(Color.FromArgb(StartColor.A, StartColor.R, StartColor.G, StartColor.B));
            var outColor =
                System.Windows.Media.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
            var solidColor = new SolidColorBrush(outColor);

            HighlightMaterials[model3D] = new DiffuseMaterial(solidColor);
        }
    }

    protected virtual void DrawAccentHighligths()
    {
        var ColorHighlight = (Brush)Application.Current.Resources["AccentButtonBackground"];
        var MaterialHighlight = new DiffuseMaterial(ColorHighlight);

        foreach (Model3DGroup model3D in model3DGroup.Children)
            // generic material(s)
            HighlightMaterials[model3D] = MaterialHighlight;
    }

    #region IDisposable Support

    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // Clear any collections holding references to 3D models or materials.
                ButtonMap?.Clear();
                DefaultMaterials?.Clear();
                HighlightMaterials?.Clear();

                // Remove all children from the model group.
                model3DGroup?.Children.Clear();
            }

            // Free unmanaged resources (if any) here.

            disposedValue = true;
        }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~IModel()
    {
        Dispose(false);
    }

    #endregion
}