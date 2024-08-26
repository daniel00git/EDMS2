/*using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Abstractions;

namespace WebApplication2.Services
{
  public class MsalAuthenticationProvider : IAuthenticationProvider
  {
    private readonly ITokenAcquisition _tokenAcquisition;

    public MsalAuthenticationProvider(ITokenAcquisition tokenAcquisition)
    {
      _tokenAcquisition = tokenAcquisition;
    }

    public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object> additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
      var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });
      request.Headers["Authorization"] = new List<string> { $"Bearer {accessToken}" };
    }
  }
}
*/
