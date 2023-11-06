namespace InterfaceComposition.REST.UrlParameters.Traits
{
    public interface IWithCommaSeparatedArrayParamValues : IWithArrayUrlParameter
    {
        string IUrlParameter.ToUrlEncodedString()
        {
            return $"{EncodedKey}={string.Join(",", GetUrlEncodedArrayValues())}";
        }
    }
}
