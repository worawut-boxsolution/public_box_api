using public_box_api.application.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace public_box_api.application.Interfaces.Repositories
{
    public class IAuthenticationRepository : IAuthenticationService
    {
        //public async Task<AuthResult> GenerateJwtToken(MemberDetail user)
        //{
        //    try
        //    {
        //        var jwtTokenHandler = new JwtSecurityTokenHandler();


        //        var securityKey = Encoding.UTF8.GetBytes(_jwtConfig.Key);


        //        var tokenDescriptor = new SecurityTokenDescriptor
        //        {
        //            Subject = new ClaimsIdentity(new[]
        //            {
        //            new Claim("Id", user.policy_number + user.member_number),
        //            //new Claim(JwtRegisteredClaimNames.Birthdate, user.CLTDOB),
        //            //new Claim(JwtRegisteredClaimNames.GivenName, user.LGIVNAME),
        //            //new Claim(JwtRegisteredClaimNames.FamilyName, user.LSURNAME),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        }),
        //            Expires = DateTime.Now.AddSeconds((int)_jwtConfig.DurationInMinutes * 60), // 5-10 
        //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
        //        };

        //        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        //        var jwtToken = jwtTokenHandler.WriteToken(token);

        //        var RefreshToken = new TokenData()
        //        {
        //            JwtId = token.Id,
        //            IsUsed = false,
        //            IsRevorked = false,
        //            UserId = user.policy_number + user.member_number,
        //            CreateDate = DateTime.Now,
        //            ExpiryDate = DateTime.Now.AddMinutes(_jwtConfig.DurationInMinutes),
        //            Token = RandomString(35) + Guid.NewGuid()
        //        };

        //        await _context.TokenDatas.AddAsync(RefreshToken);
        //        await _context.SaveChangesAsync();

        //        return new AuthResult()
        //        {
        //            token = jwtToken,
        //            success = true,
        //            refresh_token = RefreshToken.Token,
        //            expires_in = (int)_jwtConfig.DurationInMinutes * 60
        //        };
        //    }
        //    catch (Exception Ex)
        //    {
        //        return new AuthResult()
        //        {

        //            success = false,
        //            errors = new List<string>() {
        //                    Ex.ToString()
        //                }
        //        };
        //    }
        //}

        //public async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        //{
        //    var jwtTokenHandler = new JwtSecurityTokenHandler();

        //    try
        //    {

        //        var key = Encoding.UTF8.GetBytes(_jwtConfig.Key);
        //        var tokenValidationParams = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ValidateLifetime = true,
        //            RequireExpirationTime = false,
        //            ClockSkew = TimeSpan.Zero

        //        };
        //        // Validation 1 - Validation JWT token format
        //        var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.token, tokenValidationParams, out var validatedToken);

        //        // Validation 2 - Validate encryption alg
        //        if (validatedToken is JwtSecurityToken jwtSecurityToken)
        //        {
        //            var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        //            if (result == false)
        //            {
        //                return null;
        //            }
        //        }

        //        // Validation 3 - validate expiry date
        //        var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //        var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

        //        if (expiryDate > DateTime.Now)
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token has not yet expired"
        //                }
        //            };
        //        }

        //        // validation 4 - validate existence of the token
        //        var storedToken = await _context.TokenDatas.FirstOrDefaultAsync(x => x.Token == tokenRequest.refresh_token);

        //        if (storedToken == null)
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token does not exist"
        //                }
        //            };
        //        }

        //        // Validation 5 - validate if used
        //        if (storedToken.IsUsed)
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token has been used"
        //                }
        //            };
        //        }

        //        // Validation 6 - validate if revoked
        //        if (storedToken.IsRevorked)
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token has been revoked"
        //                }
        //            };
        //        }

        //        // Validation 7 - validate the id
        //        var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //        if (storedToken.JwtId != jti)
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token doesn't match"
        //                }
        //            };
        //        }

        //        // update current token 

        //        storedToken.IsUsed = true;
        //        _context.TokenDatas.Update(storedToken);
        //        await _context.SaveChangesAsync();

        //        // Generate a new token

        //        //AuthenMember InputData = new AuthenMember();
        //        //InputData.policy = storedToken.UserId.Substring(0, 7);
        //        //InputData.memberno = storedToken.UserId.Substring(8, storedToken.UserId.Length);


        //        //var dbUser = await _Authen.Get_Memberdetail_Asyn<T>(InputData);  /// a.Get_Authentication_Asyn<MemberDetail>(InputData);
        //        //return await GenerateJwtToken(dbUser.Data);

        //        return new AuthResult()
        //        {
        //            success = true,
        //            errors = new List<string>() {
        //                    "Generate New  Token"
        //                },
        //            token = storedToken.UserId
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.Contains("Lifetime validation failed. The token is expired."))
        //        {

        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Token has expired please re-login"
        //                }
        //            };

        //        }
        //        else
        //        {
        //            return new AuthResult()
        //            {
        //                success = false,
        //                errors = new List<string>() {
        //                    "Something went wrong."
        //                }
        //            };
        //        }
        //    }
        //}
    }
}
