using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Contracts;
using Server.Helpers.WebEnum;
using Server.Models.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    /// <summary>
    /// The Auth Controller responsible for API actions for Authentication action.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase, IAuthContracts
    {
        private readonly JwtSettings _jwtSettings;

        public AuthController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        /// <summary>
        /// Generates a JWT (JSON Web Token) for authentication.
        /// </summary>
        /// <param name="nameToken">The username or unique identifier for the token.</param>
        /// <param name="role">The role associated with the user.</param>
        /// <returns>
        ///     Returns a JWT token with the specified claims.
        ///     If successful, returns an object containing:
        ///     - `success`: Boolean indicating success.
        ///     - `token`: The generated JWT string.
        ///     - `expiresAt`: The expiration time of the token.
        /// </returns>
        [HttpGet("GenerateToken")]
        public IActionResult GenerateToken(Roles role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds
            );
            return Ok(new
            {
                success = true,
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiresAt = token.ValidTo
            });
        }
    }
}
