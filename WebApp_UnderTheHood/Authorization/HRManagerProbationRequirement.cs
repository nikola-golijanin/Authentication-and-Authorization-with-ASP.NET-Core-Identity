using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization; 

public class HRManagerProbationRequirement : IAuthorizationRequirement
{
    public int ProbationMonths { get; }
    
    public HRManagerProbationRequirement(int probationMonths)
        {
            ProbationMonths = probationMonths;
        }
}