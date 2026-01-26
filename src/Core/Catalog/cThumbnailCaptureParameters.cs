namespace ReCap.Parser.Catalog;

public sealed class cThumbnailCaptureParameters : AssetCatalog
{
    protected override void Build()
    {
        Struct("cThumbnailCaptureParameters", 108,
            Field("fovY", DataType.Float, 0),
            Field("nearPlane", DataType.Float, 0),
            Field("farPlane", DataType.Float, 0),
            IStruct("cameraPosition", "cSPVector3", 0),
            Field("cameraScale", DataType.Float, 0),
            IStruct("cameraRotation_0", "cSPVector3", 0),
            IStruct("cameraRotation_1", "cSPVector3", 0),
            IStruct("cameraRotation_2", "cSPVector3", 0),
            Field("mouseCameraDataValid", DataType.Bool, 0),
            Field("mouseCameraOffset", DataType.Float, 0),
            IStruct("mouseCameraSubjectPosition", "cSPVector3", 0),
            Field("mouseCameraTheta", DataType.Float, 0),
            Field("mouseCameraPhi", DataType.Float, 0),
            Field("mouseCameraRoll", DataType.Float, 0),
            Field("poseAnimID", DataType.UInt, 0)
        );
    }
}
