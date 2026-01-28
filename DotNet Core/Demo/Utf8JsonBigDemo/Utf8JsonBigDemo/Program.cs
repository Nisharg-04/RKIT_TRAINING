using System;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Utf8JsonBigDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Reading 
            Console.WriteLine("Reading JSON");
            byte[] jsonBytes = Encoding.UTF8.GetBytes(Constants.bigJson);
            var reader = new Utf8JsonReader(jsonBytes);
            // Variables to store extracted values
            string requestId = null;
            string level = null;
            string message = null;
            int? amount = null;
            string currency = null;
            List<String> cardDetails = new List<string>();

            // Track where we are 
            bool insidePaymentObject = false;
            bool insideCardDetails = false;
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                    reader.Read(); // Move to value

                    switch (propertyName)
                    {
                        case "requestId":
                            requestId = reader.GetString();
                            break;

                        case "level":
                            level = reader.GetString();
                            break;

                        case "message":
                            message = reader.GetString();
                            break;

                        case "payment":
                            insidePaymentObject = true;
                            break;

                        case "amount" when insidePaymentObject:
                            amount = reader.GetInt32();
                            break;

                        case "currency" when insidePaymentObject:
                            currency = reader.GetString();
                            break;
                        case "cardDetails" when insidePaymentObject:
                            insideCardDetails = true;
                            break;
                        case "last4" when insideCardDetails:
                            cardDetails.Add(propertyName + " : " + reader.GetString());
                            break;
                        case "issuer" when insideCardDetails:
                            cardDetails.Add(propertyName + " : " + reader.GetString());
                            break;
                        case "network" when insideCardDetails:
                            cardDetails.Add(propertyName + " : " + reader.GetString());
                            break;




                    }
                }

                // Exit payment object
                if (reader.TokenType == JsonTokenType.EndObject && insidePaymentObject)
                {
                    insidePaymentObject = false;
                }
            }



            //Writing 
            using var stream = new MemoryStream();
            using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions
            {
                Indented = true
            });

            writer.WriteStartObject();
            writer.WriteString("requestId", requestId);
            writer.WriteString("level", level);
            writer.WriteString("message", message);
            writer.WriteStartObject("payment");
            writer.WriteNumber("amount", amount ?? 0);
            writer.WriteString("currency", currency);
            writer.WriteStartArray("Credit card Details");
            writer.WriteStringValue(cardDetails[0].ToString());
            writer.WriteStringValue(cardDetails[1].ToString());

            writer.WriteStringValue(cardDetails[2].ToString());

            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.WriteEndObject();

            writer.Flush();

            string optimizedJson = Encoding.UTF8.GetString(stream.ToArray());

            Console.WriteLine("OPTIMIZED JSON OUTPUT");
            Console.WriteLine(optimizedJson);

        }
    }
}