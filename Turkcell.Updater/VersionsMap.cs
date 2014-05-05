using System;
using System.Collections.Generic;
using LitJson;
using Turkcell.Updater.LitJson;
using Turkcell.Updater.Utility;

namespace Turkcell.Updater
{
    internal class VersionsMap
    {
        private readonly List<MessageEntry> _messageEntries;
        private readonly String _packageName;
        private readonly List<UpdateEntry> _updateEntries;

        internal VersionsMap(String packageName, List<UpdateEntry> updateEntries, List<MessageEntry> messageEntries)
        {
            _packageName = StringUtils.RemoveWhiteSpaces(packageName);
            _updateEntries = updateEntries;
            _messageEntries = messageEntries;

            Validate();
        }

        internal VersionsMap(JsonData jsonObject)
        {
            _packageName = StringUtils.RemoveWhiteSpaces(jsonObject.OptString("packageName", null));
            _updateEntries = new List<UpdateEntry>();

            JsonData updatesList = jsonObject.OptJsonData("updates");
            if (updatesList != null)
            {
                for (int i = 0; i < updatesList.Count; i++)
                {
                    JsonData o = updatesList[i];
                    if (o != null)
                    {
                        try
                        {
                            var ue = new UpdateEntry(o);
                            _updateEntries.Add(ue);
                        }
                        catch (Exception e)
                        {
                            Log.E("Error occured while processing update entry. Entry is omitted.", e);
                        }
                    }
                }
            }

            _messageEntries = new List<MessageEntry>();

            JsonData messagesList = jsonObject.OptJsonData("messages");
            if (messagesList != null)
            {
                for (int i = 0; i < messagesList.Count; i++)
                {
                    JsonData o = messagesList[i];
                    if (o != null)
                    {
                        try
                        {
                            var me = new MessageEntry(o);
                            _messageEntries.Add(me);
                        }
                        catch (Exception e)
                        {
                            Log.E("Error occured while processing message entry. Entry is omitted.", e);
                        }
                    }
                }
            }

            Validate();
        }

        internal static bool IsVersionMapOfPackageId(String packageName, JsonData jsonObject)
        {
            if (jsonObject == null)
            {
                return false;
            }

            packageName = StringUtils.RemoveWhiteSpaces(packageName);
            if (packageName.Length < 1)
            {
                return false;
            }

            String s = jsonObject.OptString("packageName", null);
            s = StringUtils.RemoveWhiteSpaces(s);

            return packageName.Equals(s);
        }

        internal Update GetUpdate(Properties currentProperties)
        {
            if (_updateEntries == null)
            {
                return null;
            }

            foreach (UpdateEntry updateEntry in _updateEntries)
            {
                if (updateEntry == null)
                {
                    continue;
                }
                try
                {
                    if (updateEntry.ShouldDisplay(currentProperties))
                    {
                        return updateEntry.GetUpdate(currentProperties);
                    }
                }
                catch (Exception e)
                {
                    Log.E("Error occured while searching update entry to display", e);
                }
            }
            return null;
        }

        internal Message GetMessage(Properties currentProperties, MessageDisplayRecords records)
        {
            if (_messageEntries == null)
            {
                return null;
            }
            foreach (MessageEntry entry in _messageEntries)
            {
                if (entry == null)
                {
                    continue;
                }
                try
                {
                    if (entry.ShouldDisplay(currentProperties, records))
                    {
                        return entry.GetMessageToDisplay(currentProperties, records);
                    }
                }
                catch (Exception e)
                {
                    Log.E("Error occured while searching message entry to display", e);
                }
            }
            return null;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(_packageName))
            {
                throw new UpdaterException(
                    "'packageName' shoud not be a null or empty.");
            }
        }
    }
}