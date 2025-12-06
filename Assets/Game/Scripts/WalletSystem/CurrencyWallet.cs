using SaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WalletSystem
{
    public class CurrencyWallet
    {
        private readonly SaveManager _saveManager;

        private CurrencyConfig[] _currency;

        public IReadOnlyList<CurrencyConfig> Currencies => _currency;

        public event Action<WalletOperationData> CurrencyCountChanged;
        public event Action<WalletOperationData, int> CurrencyIncreased;
        public event Action<WalletOperationData> CurrencyReduced;

        public CurrencyWallet(SaveManager saveManager, CurrencyConfig[] currencyConfigs)
        {
            _saveManager = saveManager;

            if (currencyConfigs.Length == 0)
            {
                throw new Exception("No currencies configs found.");
            }

            _currency = new CurrencyConfig[currencyConfigs.Length];

            for (int i = 0; i < currencyConfigs.Length; i++)
            {
                _currency[i] = currencyConfigs[i];
            }
        }

        public int GetCount(CurrencyConfig currencyConfig)
        {
            if (_currency.Contains(currencyConfig))
            {
                SaveData saveData = _saveManager.Data;

                if (saveData.currencies.ContainsKey(currencyConfig.ID))
                {
                    return saveData.currencies[currencyConfig.ID];
                }

                saveData.currencies.Add(currencyConfig.ID, 0);
                _saveManager.Save();

                return 0;
            }

            throw new Exception($"Currency: {currencyConfig.ID} not found.");
        }

        public bool TryIncrease(WalletOperationData operationData)
        {
            if (operationData.count < 0)
            {
                return false;
            }

            if (_currency.Contains(operationData.currencyConfig))
            {
                int currentCount = GetCount(operationData.currencyConfig);
                int newCount = currentCount + operationData.count;

                _saveManager.Data.currencies[operationData.currencyConfig.ID] = newCount;
                _saveManager.Save();

                WalletOperationData walletOperationData = new WalletOperationData()
                {
                    currencyConfig = operationData.currencyConfig,
                    count = newCount
                };

                CurrencyCountChanged?.Invoke(walletOperationData);
                CurrencyIncreased?.Invoke(walletOperationData, operationData.count);

                return true;
            }
            else
            {
                throw new Exception($"Currency: {operationData.currencyConfig.ID} not found.");
            }
        }

        public bool TryReduce(WalletOperationData operationData)
        {
            if (operationData.count < 0)
            {
                return false;
            }

            int currentCount = GetCount(operationData.currencyConfig);

            if (currentCount < operationData.count)
            {
                return false;
            }

            if (_currency.Contains(operationData.currencyConfig))
            {
                currentCount -= operationData.count;
                _saveManager.Data.currencies[operationData.currencyConfig.ID] = currentCount;
                _saveManager.Save();

                WalletOperationData walletOperationData = new WalletOperationData()
                {
                    currencyConfig = operationData.currencyConfig,
                    count = currentCount
                };

                CurrencyCountChanged?.Invoke(walletOperationData);
                CurrencyReduced?.Invoke(walletOperationData);

                return true;
            }
            else
            {
                throw new Exception($"Currency: {operationData.currencyConfig.ID} not found.");
            }
        }
    }
}