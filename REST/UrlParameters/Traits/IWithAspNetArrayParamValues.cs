namespace InterfaceComposition.REST.UrlParameters.Traits
{
    public interface IWithAspNetArrayParamValues : IWithArrayUrlParameter
    {
        string IUrlParameter.ToUrlEncodedString()
        {
            // ASP.NET does not accept comma-separate array params
            // Instead, we need to use the format described below:
            // https://stackoverflow.com/questions/9508265/how-do-i-accept-an-array-as-an-asp-net-mvc-controller-action-parameter
            return string.Join("&", GetUrlEncodedArrayValues().Select(encodedValue => $"{EncodedKey}={encodedValue}"));
        }
    }
}
