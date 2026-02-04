namespace AssetData.Parser.Catalog;

public sealed class cThumbnailCaptureParameters : AssetCatalog
{
    protected override void Build()
    {
        Struct("cThumbnailCaptureParameters", 108,
            Field("fovY", DataType.Float, 0),
            Field("nearPlane", DataType.Float, 0),
            Field("farPlane", DataType.Float, 0),
            Vector3("cameraPosition", 0),
            Field("cameraScale", DataType.Float, 0),
            Vector3("cameraRotation_0", 0),
            Vector3("cameraRotation_1", 0),
            Vector3("cameraRotation_2", 0),
            Field("mouseCameraDataValid", DataType.Bool, 0),
            Field("mouseCameraOffset", DataType.Float, 0),
            Vector3("mouseCameraSubjectPosition", 0),
            Field("mouseCameraTheta", DataType.Float, 0),
            Field("mouseCameraPhi", DataType.Float, 0),
            Field("mouseCameraRoll", DataType.Float, 0),
            Field("poseAnimID", DataType.UInt32, 0)
        );
    }
}
