using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Worker.Commands;

namespace Worker.Converters
{
    internal class CommandConverter : JsonCreationConverter<WorkerCommand>
    {
        protected override WorkerCommand Create(Type objectType, JObject jObject)
        {
            if (FieldExists("Command", jObject))
            {
                var value = jObject["Command"].Value<string>();
                var parameters = jObject["Parameters"].Values<string>().ToArray();
                switch (value)
                {
                    case "Login":
                        return new Login(parameters);
                    case "GoToUrl":
                        return new GoToUrl(parameters);
                    case "ClickElement":
                        return new ClickElement(parameters);
                    case "ElementExists":
                        return new ElementExists(parameters);
                    case "GiveInput":
                        return new GiveInput(parameters);
                }
            }
            return null;
        }

        private bool FieldExists(string fieldName, JObject jObject)
        {
            return jObject[fieldName] != null;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
