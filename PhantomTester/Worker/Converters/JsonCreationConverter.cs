using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Worker.Converters
{
    public abstract class JsonCreationConverter<T> : JsonConverter
    {
        protected abstract T Create(Type objectType, JObject jObject);
        public override object ReadJson(JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            // Load JObject from stream
            var jObject = JObject.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        public override bool CanWrite
        {
            get { return false; }
        }
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }
}
