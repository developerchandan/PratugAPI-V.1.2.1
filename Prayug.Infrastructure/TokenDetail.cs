using Prayug.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prayug.Infrastructure
{
    public class TokenDetail
    {
        public static TokenInfo GetTokenInfo(ClaimsPrincipal principal)
        {
            using (TokenInfo tokenInfo = new TokenInfo())
            {

                tokenInfo.id = Convert.ToInt32(principal?.Claims?.SingleOrDefault(p => p.Type == "id")?.Value);
                tokenInfo.role = Convert.ToString(principal?.Claims?.SingleOrDefault(p => p.Type == ClaimTypes.Role)?.Value);
                tokenInfo.uname = Convert.ToString(principal?.Claims?.SingleOrDefault(p => p.Type == "uname")?.Value);
                tokenInfo.udes = Convert.ToString(principal?.Claims?.SingleOrDefault(p => p.Type == "udes")?.Value);
                tokenInfo.unit = Convert.ToInt32(principal?.Claims?.SingleOrDefault(p => p.Type == "unit")?.Value);
                tokenInfo.org = Convert.ToString(principal?.Claims?.SingleOrDefault(p => p.Type == "org")?.Value);
                tokenInfo.mng = Convert.ToString(principal?.Claims?.SingleOrDefault(p => p.Type == "mng")?.Value);
                tokenInfo.atype = Convert.ToInt32(principal?.Claims?.SingleOrDefault(p => p.Type == "atype")?.Value);
                return tokenInfo;
            }

        }
    }
}
