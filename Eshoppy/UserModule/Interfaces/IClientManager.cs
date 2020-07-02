﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshoppy.UserModule.Interfaces
{
    public interface IClientManager
    {
        void RegisterUser();
        void RegisterOrg();
        void ChangeUserAccount();
        void ChangeOrgAccount();
        void SearchHistory();
        void GetClientById();
        void AddFunds();
    }
}
