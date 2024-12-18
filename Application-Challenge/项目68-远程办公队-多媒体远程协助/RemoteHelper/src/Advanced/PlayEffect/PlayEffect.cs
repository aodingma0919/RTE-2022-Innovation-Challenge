﻿/*
 * [Audio Effect]Key Step：
 * 1. Create Engine and initialize：（CreateAgoraRtcEngine、Initialize、[SetLogFile]、[InitEventHandler]）
 * 
 * 2. Join Channel：（[EnableAudio]、JoinChannel）
 * 
 * 3. PlayEffect PauseEffect  ResumeEffect StopEffect 
 * 
 * 4. Leave Channel：（LeaveChannel）
 * 
 * 5. Exit：（Dispose）
 */

using System;
using agora.rtc;

namespace RTRemoteHelper
{
    public class AudioEffect : IEngine
    {
        private string app_id_ = "";
        private string channel_id_ = "";
        private readonly string AudioEffect_TAG = "[AudioEffect] ";
        private readonly string agora_sdk_log_file_path_ = "agorasdk.log";
        private IAgoraRtcEngine rtc_engine_ = null;
        private IAgoraRtcEngineEventHandler event_handler_ = null;
        private int soundId_ = -1;
        internal override int Init(string appId, string channelId)
        {
            int ret = -1;

            app_id_ = appId;
            channel_id_ = channelId.Split(';').GetValue(0).ToString();

            if (null == rtc_engine_)
            {
                rtc_engine_ = AgoraRtcEngine.CreateAgoraRtcEngine();
            }

            LogConfig log_config = new LogConfig(agora_sdk_log_file_path_);
            RtcEngineContext rtc_engine_ctx = new RtcEngineContext(app_id_, AREA_CODE.AREA_CODE_GLOB, log_config);
            ret = rtc_engine_.Initialize(rtc_engine_ctx);
            

            event_handler_ = new AudioEffectEventHandler();
            rtc_engine_.InitEventHandler(event_handler_);
            ret = rtc_engine_.EnableAudio();
            
            return ret;
        }

        internal override int UnInit()
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                if (soundId_ >= 0) rtc_engine_.StopEffect(soundId_);
                ret = rtc_engine_.LeaveChannel();
                

                rtc_engine_.Dispose();
                rtc_engine_ = null;
            }
            return ret;
        }

        internal override int JoinChannel()
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
              
                ret = rtc_engine_.JoinChannel("", channel_id_, "info");
                
            }
            return ret;
        }

        internal override int LeaveChannel()
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                ret = rtc_engine_.LeaveChannel();
                
            }
            return ret;
        }

        public override int PlayEffect(int soundId, string filePath, int loopCount, int startPos, double pitch,
          double pan, int gain, bool publish)
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                ret = rtc_engine_.PlayEffect(soundId, filePath, 100, 0, 1.0, 0.0, 100, true);
                
                if (ret == 0) soundId_ = soundId;
            }
            else
            {
                
                return -1;
            }
            return ret;
        }
        public override int StopEffect(int soundId)
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                ret = rtc_engine_.StopEffect(soundId);
                
                soundId_ = -1;
            }
            else
            {
                
                return -1;
            }
            return ret;
        }

        public override int PauseEffect(int soundId)
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                if (soundId_ == 0)
                {
                    ret = rtc_engine_.PauseEffect(soundId);
                    
                }
                else
                {
                    
                }
            }
            else
            {
                
                return -1;
            }
            return ret;
        }
        public override int ResumeEffect(int soundId)
        {
            int ret = -1;
            if (null != rtc_engine_)
            {
                if (soundId_ == 0)
                {
                    ret = rtc_engine_.ResumeEffect(soundId);
                
                }
                
            }
            else
            {
                
                return -1;
            }
            return ret;
        }
        internal override string GetSDKVersion()
        {
            if (null == rtc_engine_)
                return "-" + (ERROR_CODE_TYPE.ERR_NOT_INITIALIZED).ToString(); 

            return rtc_engine_.GetVersion();
        }

        internal override IAgoraRtcEngine GetEngine()
        {
            return rtc_engine_;
        }
    }

    // override if need
    internal class AudioEffectEventHandler : IAgoraRtcEngineEventHandler
    {
        public override void OnWarning(int warn, string msg)
        {
            Console.WriteLine("=====>OnWarning {0} {1}", warn, msg);
        }

        public override void OnError(int error, string msg)
        {
            Console.WriteLine("=====>OnError {0} {1}", error, msg);
        }

        public override void OnJoinChannelSuccess(string channel, uint uid, int elapsed)
        {
            Console.WriteLine("----->OnJoinChannelSuccess channel={0} uid={1}", channel, uid);
        }

        public override void OnRejoinChannelSuccess(string channel, uint uid, int elapsed)
        {
            Console.WriteLine("----->OnRejoinChannelSuccess");
        }

        public override void OnLeaveChannel(RtcStats stats)
        {
            Console.WriteLine("----->OnLeaveChannel duration={0}", stats.duration);
        }

        public override void OnUserJoined(uint uid, int elapsed)
        {
            Console.WriteLine("----->OnUserJoined uid={0} elapsed={1}", uid, elapsed);
        }

        public override void OnUserOffline(uint uid, USER_OFFLINE_REASON_TYPE reason)
        {
            Console.WriteLine("----->OnUserOffline reason={0}", reason);
        }
    }
}
