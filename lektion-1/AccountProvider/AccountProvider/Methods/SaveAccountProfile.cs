using AccountProvider.Data.Contexts;
using AccountProvider.Data.Entities;
using AccountProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountProvider.Methods;

public class SaveAccountProfile(ILogger<SaveAccountProfile> logger, DataContext context)
{
    private readonly ILogger<SaveAccountProfile> _logger = logger;
    private readonly DataContext _context = context;


    [Function("SaveAccountProfile")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "account")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<CreateRequest>(body);

        var accountEntity = new AccountEntity
        {
            UserId = data!.UserId,
            FirstName = data.FirstName,
            LastName = data.LastName,
        };

        _context.Add(accountEntity);
        await _context.SaveChangesAsync();

        _logger.LogInformation("User account was saved");
        return new OkResult();
    }
}
