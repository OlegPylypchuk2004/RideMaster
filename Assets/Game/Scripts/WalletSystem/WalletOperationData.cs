using System;

namespace WalletSystem
{
    [Serializable]
    public class WalletOperationData
    {
        public CurrencyConfig currencyConfig;
        public int count;
    }
}