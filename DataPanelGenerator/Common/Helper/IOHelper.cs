using CaseExtensions;

namespace DataPanelGenerator.Common.Helper;

public static class IOHelper
{
    public static string GetPanelDataName(string _filepath)
    {
        var _imageName = Path.GetFileNameWithoutExtension(_filepath);
        return _imageName.ToSnakeCase();
    }
}