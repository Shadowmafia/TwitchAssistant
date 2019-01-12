using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchMiniChat
{
    public class AppearanceJavaScriptsExecuter
    {
        private ChromiumWebBrowser chromiumWebBrowser;
        public AppearanceJavaScriptsExecuter(ChromiumWebBrowser webBrowser)
        {
            chromiumWebBrowser = webBrowser;
        }
        public void ShowChatSettingsAndInputScript()
        {
            string showScript = @"
            var chatHeader = document.getElementsByClassName('room-selector__open-header-wrapper');
            for (i = 0; i < chatHeader.length; i++) {
               chatHeader[i].style.display = 'block';
            };
            var chatHeaderClosed = document.getElementsByClassName('room-selector__header');
            for (i = 0; i < chatHeaderClosed.length; i++) {
                chatHeaderClosed[i].classList.add('tw-flex');
                chatHeaderClosed[i].style.display = 'block';
            };
            var roomPicker = document.getElementsByClassName('room-picker');
            for (i = 0; i < roomPicker.length; i++) {
                roomPicker[i].style.display = 'block';
            };
            var chatInput = document.getElementsByClassName('chat-input');
            for (i = 0; i < chatInput.length; i++) {
                chatInput[i].classList.add('tw-block');
                chatInput[i].style.display = 'block';
            };
            ";
            ExecuteScript(showScript);
        }
        public void HideChatSettingsAndInputScript()
        {
            string hideScript = @"   
            var chatHeader = document.getElementsByClassName('room-selector__open-header-wrapper');
            for (i = 0; i < chatHeader.length; i++) {
               chatHeader[i].style.display = 'none';
            };
            var chatHeaderClosed = document.getElementsByClassName('room-selector__header');
            for (i = 0; i < chatHeaderClosed.length; i++) {
                chatHeaderClosed[i].classList.remove('tw-flex');
                chatHeaderClosed[i].style.display = 'none';
            };
            var roomPicker = document.getElementsByClassName('room-picker');
            for (i = 0; i < roomPicker.length; i++) {
                roomPicker[i].style.display = 'none';
            };
            var chatInput = document.getElementsByClassName('chat-input');
            for (i = 0; i < chatInput.length; i++) {
                chatInput[i].classList.remove('tw-block');
                chatInput[i].style.display = 'none';
            };
            ";
            ExecuteScript(hideScript);
        }
        public void HideChatSettingsAndInputScriptWithDeley()
        {
            string hideScript = @"
            setTimeout(function(){
                var chatHeader = document.getElementsByClassName('room-selector__open-header-wrapper');
                for (i = 0; i < chatHeader.length; i++) {
                   chatHeader[i].style.display = 'none';
                };
                var chatHeaderClosed = document.getElementsByClassName('room-selector__header');
                for (i = 0; i < chatHeaderClosed.length; i++) {
                    chatHeaderClosed[i].classList.remove('tw-flex');
                    chatHeaderClosed[i].style.display = 'none';
                };
                var roomPicker = document.getElementsByClassName('room-picker');
                for (i = 0; i < roomPicker.length; i++) {
                    roomPicker[i].style.display = 'none';
                };
                var chatInput = document.getElementsByClassName('chat-input');
                for (i = 0; i < chatInput.length; i++) {
                    chatInput[i].classList.remove('tw-block');
                    chatInput[i].style.display = 'none';
                };
            },200);
            ";
            ExecuteScript(hideScript);
        }


        public void PreparationForBackGround()
        {
            string twitchCustomConfigScript = @"
            var chatElements = document.getElementsByClassName('tw-c-background-alt');
            chatElements[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');       
  
            var chatRoot = document.getElementsByClassName('twilight-minimal-root');
            chatRoot[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');

            var root = document.getElementsByClassName('tw-root--theme-light');
            chatElements[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');

            var chatBorders = document.getElementsByClassName('tw-border-r');
            for (i = 0; i < chatBorders.length; i++) {
                chatBorders[i].setAttribute('style', 'border-right:none !important');
            };
            var chatBorders = document.getElementsByClassName('tw-border-l');
            for (i = 0; i < chatBorders.length; i++) {
                chatBorders[i].setAttribute('style', 'border-left:none !important');
            }
            var popout = document.getElementsByClassName('chat-room');
            popout[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');
            var chatElements = document.getElementsByClassName('tw-root--theme-light');
            chatElements[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');
            var popout = document.getElementsByClassName('popout-chat-page');
            popout[0].setAttribute('style', 'background:rgba(255,255,255,0) !important');
            ";
            ExecuteScript(twitchCustomConfigScript);
        }
        public void ChangeBackGroundColorScript(string R,string G,string B,string A)
        {
            string changeColorScript = $@"
            var ChatBody = document.getElementsByTagName('body');
            ChatBody[0].setAttribute('style', 'background:rgba({R},{G},{B},{A}) !important');       
            ";
            ExecuteScript(changeColorScript);
        }

        private void ExecuteScript(string script)
        {
            chromiumWebBrowser.ExecuteScriptAsync(script);
        }

    }
}
