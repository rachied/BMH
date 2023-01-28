using System;
using System.IO;
using System.Web.Http;
using BuyMyHouse.Application.Dto.Error;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BuyMyHouse.AzureFunctions;

public static class HelperExtensions
{
    public static Option<T> DeserializeModel<T>(this HttpRequest request)
    {
        try
        {
            using var reader = new StreamReader(request.Body);
            using var textReader = new JsonTextReader(reader);
            request.Body.Seek(0, SeekOrigin.Begin);
            var serializer = JsonSerializer.Create(new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            return serializer.Deserialize<T>(textReader);
        }
        catch (Exception e)
        {
            // TODO: Log exception
            return Option<T>.None;
        }
    }

    public static IActionResult ToActionResult(this BmhError error)
    {
        switch (error.Type)
        {
            case BmhErrorType.ValidationError:
                return new BadRequestObjectResult(error.ValidationResult?.ToDictionary());
            case BmhErrorType.PersistenceFailure:
                return new InternalServerErrorResult();
            default:
                throw new ArgumentOutOfRangeException($"No ActionResult mapping exists for BmhError: {error.Type} {error.Message}");
        }
    }
}