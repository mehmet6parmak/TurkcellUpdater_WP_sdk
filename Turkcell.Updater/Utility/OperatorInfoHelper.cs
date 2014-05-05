using System;
using Microsoft.Phone.Net.NetworkInformation;

namespace Turkcell.Updater.Utility
{
    internal class OperatorInfoHelper
    {
        private const string TurkcellLong = "turkcell";
        private const string TurkcellShort = "tcell";

        private const string VodafoneLong = "vodafone";
        private const string VodafoneShort = "vf";

        private const string Avea = "avea";

        internal static OperatorInfo GetOperatorInfo()
        {
            if (!String.IsNullOrEmpty(DeviceNetworkInformation.CellularMobileOperator))
            {
                string operatorName = DeviceNetworkInformation.CellularMobileOperator.ToLowerInvariant();

                if (operatorName.Contains(Avea))
                {
                    //learn whether it can be 04 or not, aycell was 04, how can i differentiate 03 and 04
                    //It can be whether 03 or 04 but we try to identify the operator here, 03 or 04 is not important for us. 
                    return new OperatorInfo {MobileNetworkCode = "03"};
                }
                if (operatorName.Contains(VodafoneLong) || operatorName.Contains(VodafoneShort))
                {
                    return new OperatorInfo {MobileNetworkCode = "02"};
                }
                if (operatorName.Contains(TurkcellLong) || operatorName.Contains(TurkcellShort))
                {
                    return new OperatorInfo {MobileNetworkCode = "01"};
                }
            }
            return null;
            //throw new ArgumentNullException("Could not retrieve operator information.");
        }
    }
}