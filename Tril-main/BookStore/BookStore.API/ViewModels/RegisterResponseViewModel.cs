﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.API.ViewModels
{
    public class RegisterResponseViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string token { get; set; }
    }
}
