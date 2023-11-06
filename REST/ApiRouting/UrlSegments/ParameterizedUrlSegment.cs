using System;

namespace InterfaceComposition.REST.ApiRouting.UrlSegments
{
    public class ParameterizedUrlSegment : BaseUrlSegment
    {
        public string ParameterName { get; }
        public object ParameterValue { get; set; }
        public override string Template => "{" + ParameterName + "}";
        public override string Text => ParameterValue.ToString() ??
            throw new ArgumentNullException("Route parameter has null string value");

        public ParameterizedUrlSegment(string parameterName, object parameterValue)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
        }
    }
}
