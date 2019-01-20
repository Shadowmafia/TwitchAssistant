using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DateBaseController.Context;
using DateBaseController.Models;
using DateBaseController.ModelsRepositoryes;
using Tools;

namespace DateBaseController
{
    public class AssistantDb : SingletonBaseTemplate<AssistantDb>
    {
        private AssistantDb()
        {

        }
        private TwitchAssistantContext _db = new TwitchAssistantContext();
        private readonly object _dbLocker = new object();

        private ViewerRepository _viewerRepository;
        private MessageTimerRepository _messageTimerRepository;

        private DefaultCommandRepository _defaultCommandRepository;
        private PlayerCommandRepository _playerCommandRepository;

        public ViewerRepository Viewers
        {
            get
            {
                if (_viewerRepository == null)
                    _viewerRepository = new ViewerRepository(_db);
                return _viewerRepository;
            }
        }
        public MessageTimerRepository Timers
        {
            get
            {
                if (_messageTimerRepository == null)
                    _messageTimerRepository = new MessageTimerRepository(_db);
                return _messageTimerRepository;
            }
        }

        public DefaultCommandRepository DefaultCommands
        {
            get
            {
                if (_defaultCommandRepository == null)
                    _defaultCommandRepository = new DefaultCommandRepository(_db);
                return _defaultCommandRepository;
            }
        }
        public PlayerCommandRepository PlayerCommands
        {
            get
            {
                if (_playerCommandRepository == null)
                    _playerCommandRepository = new PlayerCommandRepository(_db);
                return _playerCommandRepository;
            }
        }

        public void SaveChanges()
        {
            try
            {
                lock (_dbLocker)
                {
                    _db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                Logger.Instance.WriteLog(e.ToString(), "AssistantDb");
            }
        }

    }
}
