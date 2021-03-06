using System;

namespace Bioworld.WebApi.Security
{
    public class Extensions
    {
        private const string SectionName = "security";
        private const string RegistryName = "security";

        // public static IBioWorldBuilder AddCertificateAuthentication(this IBioWorldBuilder builder,
        //     string sectionName = SectionName, Type permissionValidatorType = null)
        // {
        //
        // }

        private static byte[] stringToByteArray(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];

            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }
    }
}