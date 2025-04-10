using System.Text.Json.Serialization;
using System.Text.Json;

namespace CurriculumATS.Application.Settings;

public class MesAnoConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString(); // Ex: "2025-04"
        if (DateTime.TryParseExact(str, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return new DateTime(date.Year, date.Month, 1); // sempre dia 1
        }

        throw new JsonException("Formato inválido. Use yyyy-MM");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Serializa apenas ano e mês
        writer.WriteStringValue(value.ToString("yyyy-MM"));
    }
}
