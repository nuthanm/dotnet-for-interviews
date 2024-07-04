
#region Input_Value_is_Not_Null

// Old Code
string inputCorrelationId = "a8f2cbf7-0388-4ef1-9f2b-fbe064f845db";
var id = !string.IsNullOrEmpty(inputCorrelationId)
? Guid.Parse(inputCorrelationId) : Guid.NewGuid();
Console.WriteLine($"Old: {id}");
// Optmized Code
var optimizedApproachId = Guid.TryParse(inputCorrelationId, out var correlationId) ? correlationId : Guid.NewGuid();
Console.WriteLine($"New: {optimizedApproachId}");

#endregion

#region Input_Value_is_null

// Old Code
string inputCorrelationIdIsnull = null;
var idWithNewGuid = !string.IsNullOrEmpty(inputCorrelationIdIsnull)
? Guid.Parse(inputCorrelationIdIsnull) : Guid.NewGuid();
Console.WriteLine($"Old: {id}");

// Optmized Code
var optimizedApproachIdTryParseNull = Guid.TryParse(inputCorrelationIdIsnull, out var correlationIdNew) ? correlationIdNew : Guid.NewGuid();
Console.WriteLine($"New: {optimizedApproachIdTryParseNull}");

#endregion
