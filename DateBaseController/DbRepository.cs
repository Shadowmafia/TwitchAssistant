using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DateBaseController.Context;
using DateBaseController.Models;
using Tools;

namespace DateBaseController
{
    public class DbRepository : SingletonBaseTemplate<DbRepository>
    {

        private TwitchAssistantContext _dbContext = new TwitchAssistantContext();
        private static object _repositoryLocker = new object();

        private List<Viewer> _viewers;
        private List<MessageTimer> _messageTimers;
        private List<DefaultCommand> _defaultCommands;
       
        private DbRepository()
        {
            _viewers = _dbContext.Viewers.ToList();
            _messageTimers = _dbContext.MessageTimers.ToList();
            _defaultCommands = _dbContext.DefaultCommands.ToList();
        }

        public List<Viewer> GetViewers()
        {
            return _viewers;           
        }
        public List<MessageTimer> GetMessageTimers()
        {
            return _messageTimers;
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
                    _dbContext.DefaultCommands.AddOrUpdate(_defaultCommands.ToArray());
                    _dbContext.Viewers.AddOrUpdate(_viewers.ToArray());
                    _dbContext.MessageTimers.AddOrUpdate(_messageTimers.ToArray());
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
