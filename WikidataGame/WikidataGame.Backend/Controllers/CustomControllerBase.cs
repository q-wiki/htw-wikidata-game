﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikidataGame.Backend.Helpers;
using WikidataGame.Backend.Models;
using WikidataGame.Backend.Repos;

namespace WikidataGame.Backend.Controllers
{
    public class CustomControllerBase : ControllerBase
    {
        protected readonly IGameRepository _gameRepo;
        protected readonly IUserRepository _userRepo;
        protected readonly IRepository<Category, string> _categoryRepo;
        protected readonly DataContext _dataContext;

        public CustomControllerBase(
            DataContext dataContext,
            IUserRepository userRepo,
            IGameRepository gameRepo,
            IRepository<Category, string> categoryRepo)
        {
            _dataContext = dataContext;
            _userRepo = userRepo;
            _gameRepo = gameRepo;
            _categoryRepo = categoryRepo;
        }

        protected User GetCurrentUser()
        {
            return _userRepo.SingleOrDefault(u => u.DeviceId == User.Identity.Name);
        }

        protected bool IsUserGameParticipant(string gameId)
        {
            var user = GetCurrentUser();
            var game = _gameRepo.Get(gameId);
            return game != null && game.GameUsers.SingleOrDefault(gu => gu.UserId == user.Id) != null;
        }
    }
}
