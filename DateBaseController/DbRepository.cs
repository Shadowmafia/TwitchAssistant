using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using DateBaseController.Context;
using DateBaseController.Models;
using Tools;

namespace DateBaseController.Repositories.Class
{
    public class DbRepository : SingletonBaseTemplate<DbRepository>
    {

        private TwitchAssistantContext _dbContext = new TwitchAssistantContext();
        private static object _repositoryLocker = new object();

        private List<Viewer> _viewers;
        private List<MessageTimer> _timers;
        private List<DefaultCommand> _defaultCommands;

        
        private DbRepository()
        {
            _viewers = _dbContext.Viewers.ToList();
            _timers = _dbContext.MessageTimers.ToList();
            _defaultCommands = _dbContext.DefaultCommands.ToList();
        }

        public List<Viewer> GetViewers()
        {
            return _viewers;           
        }

        public List<MessageTimer> GetMessageTimers()
        {
            return _timers;
        }

        public List<DefaultCommand> GetDefaultCommands()
        {
            return _defaultCommands;
        }

        public void SaveChanges()
        {
            try
            {                           
                lock (_repositoryLocker)
                {
                   
                    _dbContext.DefaultCommands.AddRange(_defaultCommands.ToList());
                    _dbContext.SaveChanges();
                }
               
            }
            catch (Exception e)
            {    
               
               Logger.Instance.WriteLog(e.ToString(),"DbRepository");                           
            }
        }
    }
}
