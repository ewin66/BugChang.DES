﻿using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BugChang.DES.Core.Authorization.Users;
using Microsoft.Extensions.Options;
using NETCore.Encrypt.Extensions;

namespace BugChang.DES.Core.Authentication
{
    public class LoginManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AccountSettings> _accountSettings;

        public LoginManager(IUserRepository userRepository, IOptions<AccountSettings> accountSettings)
        {
            _userRepository = userRepository;
            _accountSettings = accountSettings;
        }

        /// <summary>
        /// 检查用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="usbKeyNo">UKey编码</param>
        /// <returns></returns>
        public async Task<LoginResult> LoginAysnc(string userName, string password, string usbKeyNo)
        {
            var loginResult = new LoginResult();
            var user = await _userRepository.GetAsync(userName, password);
            if (user == null)
            {

                var tryCount = await AddErrorCountAsync(userName);
                if (tryCount == 0)
                {
                    loginResult.Result = EnumLoginResult.账号已锁定;
                }
                else if (tryCount > 0 || tryCount == -1)
                {
                    loginResult.Result = EnumLoginResult.用户名或密码错误;
                    if (tryCount > 0)
                    {
                        loginResult.Message = $"，剩余{tryCount}次机会！";
                    }
                }
            }
            else
            {
                if (_accountSettings.Value.ValidateUsbKeyNo && user.UsbKeyNo != usbKeyNo)
                {
                    loginResult.Result = EnumLoginResult.UKey与用户不匹配;
                    loginResult.Message = "UKey与用户不匹配";
                }
                else
                {
                    if (user.LoginErrorCount >= _accountSettings.Value.LoginErrorCount2Lock)
                    {
                        loginResult.Result = EnumLoginResult.账号已锁定;
                    }
                    else if (!user.Enabled)
                    {
                        loginResult.Result = EnumLoginResult.账号已停用;
                    }
                    else
                    {
                        if (user.Password == User.DefaultPassword.MD5() && _accountSettings.Value.ForceChangePassword)
                        {
                            loginResult.Result = EnumLoginResult.强制修改密码;
                        }
                        else
                        {
                            loginResult.Result = EnumLoginResult.登录成功;
                        }

                        ClearErrorCount(user);
                        var claims = new List<Claim>
                        {
                            new Claim("Id",user.Id.ToString()),
                            new Claim("UserName",user.UserName),
                            new Claim("DisplayName",user.DisplayName),
                            new Claim("DepartmentId",user.DepartmentId.ToString()),
                            new Claim("UsbKeyNo",user.UsbKeyNo??""),
                            new Claim("NeedChangePassword",(loginResult.Result== EnumLoginResult.强制修改密码?1:0).ToString())
                        };
                        foreach (var role in user.UserRoles)
                        {
                            claims.Add(new Claim("RoleId", role.Role.Id.ToString()));
                            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
                        }
                        var claimsIdentity = new ClaimsIdentity("BugChang.DES.Cookies");
                        claimsIdentity.AddClaims(claims);
                        loginResult.ClaimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    }
                }

            }
            return loginResult;
        }

        /// <summary>
        /// 登录错误次数+1
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public async Task<int> AddErrorCountAsync(string userName)
        {
            var user = await _userRepository.GetAsync(userName);
            if (user != null)
            {
                if (user.Locked)
                {
                    //如果用户已经被锁定，直接返回0
                    return 0;
                }
                user.LoginErrorCount += 1;
                var tryCount = _accountSettings.Value.LoginErrorCount2Lock - user.LoginErrorCount;
                if (tryCount == 0)
                {
                    //无剩余登录尝试次数，直接锁定
                    ChangeLockState(user, true);
                }
                return tryCount;
            }
            return -1;
        }

        /// <summary>
        /// 清除登录错误次数
        /// </summary>
        /// <param name="user"></param>
        public void ClearErrorCount(User user)
        {
            user.LoginErrorCount = 0;
        }

        /// <summary>
        /// 更改锁定状态
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="isLock">是否锁定</param>
        public void ChangeLockState(User user, bool isLock)
        {
            user.Locked = isLock;
        }
    }
}
