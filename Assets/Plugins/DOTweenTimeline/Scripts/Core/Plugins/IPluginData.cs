// Author: Daniele Giardini - http://www.demigiant.com
// Created: 2020/02/01

namespace DG.Tweening.Timeline.Core.Plugins
{
    public interface IPluginData
    {
        bool wantsTarget { get; }
        string label { get; }
        string targetLabel { get; }
        string stringOptionLabel { get; }
        string intOptionLabel { get; }
        DOVisualSequenced.PropertyType propertyType { get; }
    }
}