using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DateBaseController;
using Tools;

namespace TwitchAssistant.Tests.DateBaseTest
{
    class DateBaseTester : SingletonBaseTemplate<DateBaseTester>,ITest
    {
        private DateBaseTester() { }
        public bool Test()
        {
            var x = DbRepository.Instance.GetDefaultCommands();
            int itttt = 0;
            for (int i = 0; i < 50; i++)
            {
                new Thread(() =>
                {                  
                    int it = 0;
                    while (true)
                    {                     
                        x.Add(new DateBaseController.Models.DefaultCommand
                        {
                            Description = "qwe",
                            LastCommandCall = DateTime.Now,
                            Name = Interlocked.Increment(ref itttt) + ":" + it + ":qwe",
                            IsEnabled = false,
                            Message = false,
                            Whisp = false,
                        });
                        DbRepository.Instance.SaveChanges();
                        it++;
                    }                 
                }).Start();
            }

            return true;
        }
    }
}
