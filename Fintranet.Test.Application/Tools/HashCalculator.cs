using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using XSystem.Security.Cryptography;

namespace Fintranet.Test.Application.Tools
{
    public static class HashCalculator
    {
        public static string NewKey<T>(T value)
        {
            using (MemoryStream stream = new MemoryStream())
            using (SHA512Managed hash = new SHA512Managed())
            {
                XmlSerializer serialize = new XmlSerializer(typeof(T));

                serialize.Serialize(stream, value);
                stream.Position = 0;
                return Convert.ToBase64String(hash.ComputeHash(stream));
            }
        }
    }
}
