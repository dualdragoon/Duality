using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Duality.Records
{
    /// <summary>
    /// Serializes objects. Deprecated.
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Loads object from a serialized string.
        /// </summary>
        /// <typeparam name="TData">Object type to return.</typeparam>
        /// <param name="settings">Serialized string.</param>
        /// <returns>Desearialized object.</returns>
        public static TData DeserializeFromString<TData>(string settings)
        {
            byte[] b = Convert.FromBase64String(settings);
            using (var stream = new MemoryStream(b))
            {
                var formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return (TData)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="TData">Object type to serialize.</typeparam>
        /// <param name="settings">Object to serialize.</param>
        /// <returns>Serialized string.</returns>
        public static string SerializeToString<TData>(TData settings)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, settings);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
}
