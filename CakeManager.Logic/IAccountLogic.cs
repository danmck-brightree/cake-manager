﻿using CakeManager.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CakeManager.Logic
{
    public interface IAccountLogic
    {
        Task<bool> HasLocalUser();
        Task<bool> RegisterLocalUser(Guid selectedOfficeId);
        Task<List<ActiveDirectoryUser>> GetUsers();
        Task<bool> DeleteUser(string email);
        Task<bool> ToggleUserAdmin(string email);
    }
}
