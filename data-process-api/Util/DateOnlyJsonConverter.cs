using System.Text.Json;
using System.Text.Json.Serialization;


namespace data_process_api.Util {
    public class DateOnlyJsonConverter : JsonConverter<DateTime> {
        private const string DateFormat = "yyyy-MM-dd";

        public DateOnlyJsonConverter() { }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return DateTime.ParseExact(reader.GetString()!, DateFormat, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }

}
